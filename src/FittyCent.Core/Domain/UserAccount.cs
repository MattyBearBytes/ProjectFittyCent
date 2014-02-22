﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Domain {
    public class UserAccount : IdentityUser {
        public virtual string Email { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Postcode { get; set; }
        public virtual TrainerProfile TrainerProfile { get; set; }
    }
}