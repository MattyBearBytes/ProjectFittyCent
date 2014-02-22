using System.ComponentModel.DataAnnotations;
using FittyCent.Domain;

namespace FittyCent.Web.Models {
    public class EditUserAccountModel {
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

        [Display(Name = "Do you provide a mobile service?")]
        public bool HasMobileServiceAvailable { get; set; }

        [Display(Name = "Is your business insurance covered?")]
        public bool IsInsured { get; set; }

        [Display(Name = "Associations (comma seperated)")]
        public string Registrations { get; set; }

        [Display(Name = "Specialisations (comma seperated)")]
        public string Specialisations { get; set; }

        [Display(Name = "Qualifications (comma seperated)")]
        public string Qualifications { get; set; }
    }
}