using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FittyCent.Data;
using FittyCent.Domain;
using MailChimp;
using MailChimp.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using FittyCent.Web.Models;

namespace FittyCent.Web.Controllers {
    [Authorize]
    public class AccountController : Controller {
        private const string StringSchemaType = "http://www.w3.org/2001/XMLSchema#string";

        public AccountController()
            : this(new UserManager<UserAccount>(new UserStore<UserAccount>(new FitnessContext()))) {
                UserManager.UserValidator = new UserValidator<UserAccount>(UserManager) { AllowOnlyAlphanumericUserNames = false };
            }

        public AccountController(UserManager<UserAccount> userManager) {
            UserManager = userManager;
        }

        public UserManager<UserAccount> UserManager { get; private set; }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterMailchimp() {
            return Redirect("http://eepurl.com/OZO0f");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if ( ModelState.IsValid ) {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if ( user != null ) {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                } else {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model) {
            if ( ModelState.IsValid ) {
                var user = new UserAccount
                           {
                               UserName = model.Email,
                               FirstName = model.FirstName,
                               Surname = model.Surname, 
                               Email = model.Email, 
                               Postcode = model.Postcode,
                               Gender = model.Gender,
                               BirthYear = model.BirthYear,
                               UserType = model.UserType.Value, 
                               TrainerProfile = new TrainerProfile()
                           };
                var result = await UserManager.CreateAsync(user, model.Password);
                if ( result.Succeeded ) {
                    await SignInAsync(user, isPersistent: false);

                    //Add to mail chimp
                    var mc = new MailChimpManager(ConfigurationManager.AppSettings["MailChimpApiKey"]);
                    mc.Subscribe(ConfigurationManager.AppSettings["MailChimpListId"], new EmailParameter { Email = model.Email }, 
                        new { FNAME = model.FirstName, LNAME = model.Surname, POSTCODE = model.Postcode, BIRTHYEAR = model.BirthYear }, 
                        "html", false, false, false, true);

                    return RedirectToAction("Profile", "Account");
                } else {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey) {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if ( result.Succeeded ) {
                message = ManageMessageId.RemoveLoginSuccess;
            } else {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ChangePassword", new { Message = message });
        }

        public ActionResult ManageAccess(ManageMessageId? message) {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("ManageAccess");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ManageAccess(ChangePassword model) {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("ManageAccess");
            if ( hasPassword ) {
                if ( ModelState.IsValid ) {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if ( result.Succeeded ) {
                        return RedirectToAction("ManageAccess", new { Message = ManageMessageId.ChangePasswordSuccess });
                    } else {
                        AddErrors(result);
                    }
                }
            } else {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if ( state != null ) {
                    state.Errors.Clear();
                }

                if ( ModelState.IsValid ) {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if ( result.Succeeded ) {
                        return RedirectToAction("ManageAccess", new { Message = ManageMessageId.SetPasswordSuccess });
                    } else {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl) {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl) {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if ( loginInfo == null ) {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if ( user != null ) {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            } else {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        [HttpPost]
        public ActionResult LinkLogin(string provider) {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        public async Task<ActionResult> LinkLoginCallback() {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if ( loginInfo == null ) {
                return RedirectToAction("ManageAccess", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if ( result.Succeeded ) {
                return RedirectToAction("ManageAccess");
            }
            return RedirectToAction("ManageAccess", new { Message = ManageMessageId.Error });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl) {
            if ( User.Identity.IsAuthenticated ) {
                return RedirectToAction("ManageAccess");
            }

            if ( ModelState.IsValid ) {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if ( info == null ) {
                    return View("ExternalLoginFailure");
                }
                var user = new UserAccount { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if ( result.Succeeded ) {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if ( result.Succeeded ) {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff() {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure() {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList() {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult) PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        [HttpGet]
        public ActionResult Me() {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if ( user.TrainerProfile == null ) {
                user.TrainerProfile = new TrainerProfile();
            }

            var model = Mapper.Map<UserAccountModel>(user);
            return View(model);
        }

        [HttpGet]
        public ActionResult Profile() {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if ( user.TrainerProfile == null ) {
                user.TrainerProfile = new TrainerProfile();
            }

            var model = Mapper.Map<UserAccountModel>(user);
            return View(model);
        }

        [HttpGet]
        public ActionResult Classes() {
            return View();
        }

        [HttpGet]
        public ActionResult Settings() {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if ( user.TrainerProfile == null ) {
                user.TrainerProfile = new TrainerProfile();
            }

            var model = Mapper.Map<UserAccountModel>(user);
            return View(model);
        }

        [HttpGet]
        public ActionResult EditSettings() {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if ( user.TrainerProfile == null ) {
                user.TrainerProfile = new TrainerProfile();
            }

            var model = Mapper.Map<EditUserAccountModel>(user);
            return View(model);
        }

        [HttpGet]
        public ActionResult EditProfile() {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if ( user.TrainerProfile == null ) {
                user.TrainerProfile = new TrainerProfile();
            }

            var model = Mapper.Map<EditUserAccountModel>(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSettings(EditUserAccountModel model) {
            Edit(model);
            return RedirectToAction("Settings");
        }

        [HttpPost]
        public ActionResult EditProfile(EditUserAccountModel model) {
            Edit(model);
            return RedirectToAction("Profile");
        }

        private void Edit(EditUserAccountModel model) {
            if ( ModelState.IsValid ) {
                var user = UserManager.FindById(User.Identity.GetUserId());

                user.FirstName = model.FirstName;
                user.Surname = model.Surname;
                user.Postcode = model.Postcode;

                if (user.TrainerProfile == null)
                {
                    user.TrainerProfile = new TrainerProfile();
                }

                var trainerProfile = user.TrainerProfile;
                trainerProfile.Summary = model.Summary;
                
                if ( user.UserType == UserType.Trainer ) {
                    trainerProfile.CompanyName = model.CompanyName;
                    trainerProfile.CompanyWebsite = model.CompanyWebsite;
                    trainerProfile.IsInsured = model.IsInsured;
                    trainerProfile.Registrations = model.Registrations;
                    trainerProfile.Specialisations = model.Specialisations;
                    trainerProfile.Qualifications = model.Qualifications;
                    trainerProfile.HasMobileServiceAvailable = model.HasMobileServiceAvailable;
                }
                UserManager.Update(user);
            }

            return;
        }

        protected override void Dispose(bool disposing) {
            if ( disposing && UserManager != null ) {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(UserAccount user, bool isPersistent) {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            if ( user.UserType == UserType.Trainer ) {
                identity.AddClaim(new Claim(Constants.Claims.Role, Constants.Roles.Trainer, StringSchemaType));
            }

            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result) {
            foreach ( var error in result.Errors ) {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword() {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if ( user != null ) {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl) {
            if ( Url.IsLocalUrl(returnUrl) ) {
                return Redirect(returnUrl);
            } else {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null) {
            }

            public ChallengeResult(string provider, string redirectUri, string userId) {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context) {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if ( UserId != null ) {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}