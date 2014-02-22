using FittyCent.Domain;
using Microsoft.AspNet.Identity;

namespace FittyCent {
    public class UserManager : UserManager<UserAccount> {
        public UserManager(IUserStore<UserAccount> store)
            : base(store) {
            UserValidator = new UserValidator<UserAccount>(this) {
                AllowOnlyAlphanumericUserNames = false
            };
        }
    }
}