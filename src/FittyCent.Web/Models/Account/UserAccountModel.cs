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

        [Display(Name = "Bio")]
        public string Summary { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Website")]
        public string CompanyWebsite { get; set; }
        [Display(Name = "Is your business insurance covered?")]
        public bool? IsInsured { get; set; }
        [Display(Name = "Associations")]
        public string[] Registrations { get; set; }
        [Display(Name = "Specialisations")]
        public string[] Specialisations { get; set; }
        [Display(Name = "Qualifications")]
        public string[] Qualifications { get; set; }
        [Display(Name = "Do you provide a mobile service?")]
        public bool? HasMobileServiceAvailable { get; set; }
    }
}