using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Domain {
    public class UserAccount : IdentityUser {
        public string Email { get; set; }
        public UserType UserType { get; set; }
    }
}
