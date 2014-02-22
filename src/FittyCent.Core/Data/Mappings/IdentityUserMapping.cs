using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Data.Mappings {
    internal class IdentityUserMapping : EntityTypeConfiguration<IdentityUser> {
        public IdentityUserMapping() {
            ToTable("IdentityUsers");

            HasKey(x => x.Id);

            Property(x => x.UserName)
                .IsRequired();

            Property(x => x.PasswordHash)
                .IsRequired();

            Property(x => x.SecurityStamp)
                .IsOptional();

            HasMany(x => x.Roles)
                .WithRequired(x => x.User);

            HasMany(x => x.Claims)
                .WithRequired(x => x.User);

            HasMany(x => x.Logins)
                .WithRequired(x => x.User);
        }
    }
}