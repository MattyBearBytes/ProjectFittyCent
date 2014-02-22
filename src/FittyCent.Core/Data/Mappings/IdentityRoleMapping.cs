using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Data.Mappings {
    internal class IdentityRoleMapping : EntityTypeConfiguration<IdentityRole> {
        public IdentityRoleMapping() {
            ToTable("IdentityRoles");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}