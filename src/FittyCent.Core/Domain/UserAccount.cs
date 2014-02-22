using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Domain {
    public class UserAccount : IdentityUser {
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Postcode { get; set; }
        public TrainerProfile TrainerProfile { get; set; }
    }

    public class TrainerProfile {
        public string Summary { get; set; }
        public string CompanyName { get; set; }
        public bool? IsInsured { get; set; }
        public string Registrations { get; set; }
        public string Specialisations { get; set; }
        public string Qualifications { get; set; }
        public bool? HasMobileServiceAvailable { get; set; }
    }
}
