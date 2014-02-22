using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Data.Mappings {
    internal class IdentityUserLoginMapping : EntityTypeConfiguration<IdentityUserLogin> {
        public IdentityUserLoginMapping() {
            ToTable("IdentityUserLogins");

            HasKey(x => new {
                x.UserId,
                x.LoginProvider,
                x.ProviderKey
            });

            HasRequired(x => x.User)
                .WithMany();
        }
    }
}