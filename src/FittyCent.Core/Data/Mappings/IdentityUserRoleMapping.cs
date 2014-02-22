using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Data.Mappings {
    internal class IdentityUserRoleMapping : EntityTypeConfiguration<IdentityUserRole> {
        public IdentityUserRoleMapping() {
            ToTable("IdentityUserRoles");

            HasKey(x => new {
                x.UserId,
                x.RoleId
            });

            HasRequired(x => x.User)
                .WithMany();

            HasRequired(x => x.Role)
                .WithMany();
        }
    }
}