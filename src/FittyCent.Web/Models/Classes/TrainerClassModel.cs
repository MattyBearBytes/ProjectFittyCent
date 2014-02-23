using System.ComponentModel.DataAnnotations;

namespace FittyCent.Web.Models {
    public class TrainerClassModel {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Type { get; set; }

        [Display(Name = "Keywords (comma separated)")]
        public string Keywords { get; set; }

        public SessionModel[] Sessions { get; set; }
    }
}