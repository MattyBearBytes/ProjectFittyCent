using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Domain {
    public class UserAccount : IdentityUser {
        public virtual string Email { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Postcode { get; set; }
        public virtual int BirthYear { get; set; }
        public virtual Genders Gender { get; set; }
        public virtual TrainerProfile TrainerProfile { get; set; }
        public virtual ICollection<TrainerClass> Classes { get; private set; }

        public UserAccount() {
            Classes = new List<TrainerClass>();
        }
    }
}