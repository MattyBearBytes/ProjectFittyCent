using System.ComponentModel.DataAnnotations;
using FittyCent.Domain;

namespace FittyCent.Web.Models {
    public class UserAccountModel {
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "User Type")]
        public UserType UserType { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Display(Name = "Bio")]
        public string Summary { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Website")]
        public string CompanyWebsite { get; set; }
        [Display(Name = "Has Insurance?")]
        public bool? IsInsured { get; set; }
        [Display(Name = "Registrations")]
        public string[] Registrations { get; set; }
        [Display(Name = "Specialisations")]
        public string[] Specialisations { get; set; }
        [Display(Name = "Qualifications")]
        public string[] Qualifications { get; set; }
        [Display(Name = "Has Mobile Service Available?")]
        public bool? HasMobileServiceAvailable { get; set; }
    }
}