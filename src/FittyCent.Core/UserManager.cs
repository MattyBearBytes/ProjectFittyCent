using FittyCent.Domain;
using Microsoft.AspNet.Identity;

//TODO: This isn't doing anything... had to add manually to AccountController.cs instead
namespace FittyCent {
    public class UserManager : UserManager<UserAccount> {
        public UserManager(IUserStore<UserAccount> store)
            : base(store) {
            UserValidator = new UserValidator<UserAccount>(this) {
                AllowOnlyAlphanumericUserNames = false
            };
        }
    }
}