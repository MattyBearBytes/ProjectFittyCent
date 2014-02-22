using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Domain {
    public class UserAccount : IdentityUser {
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
