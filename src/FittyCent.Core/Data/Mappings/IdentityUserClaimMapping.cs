using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FittyCent.Data.Mappings {
    internal class IdentityUserClaimMapping : EntityTypeConfiguration<IdentityUserClaim> {
        public IdentityUserClaimMapping() {
            ToTable("IdentityUserClaims");

            HasKey(x => x.Id);

            Property(x => x.ClaimType)
                .IsRequired();

            Property(x => x.ClaimValue)
                .IsRequired();

            HasRequired(x => x.User)
                .WithMany()
                .Map(m => m.MapKey("UserId"));
        }
    }
}