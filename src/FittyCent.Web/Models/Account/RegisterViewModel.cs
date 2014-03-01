using System.ComponentModel.DataAnnotations;
using FittyCent.Domain;

namespace FittyCent.Web.Models {
    public class RegisterViewModel {
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [StringLength(512)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Required]
        [Display(Name = "Birth Year")]
        public int BirthYear { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Genders Gender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "User Type")]
        [Required]
        public UserType? UserType { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}