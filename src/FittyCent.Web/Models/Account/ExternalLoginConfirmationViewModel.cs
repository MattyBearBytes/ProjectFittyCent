using System.ComponentModel.DataAnnotations;

namespace FittyCent.Web.Models {
    public class ExternalLoginConfirmationViewModel {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}
