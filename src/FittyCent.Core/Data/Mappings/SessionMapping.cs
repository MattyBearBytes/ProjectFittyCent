using System.Data.Entity.ModelConfiguration;
using FittyCent.Domain;

namespace FittyCent.Data.Mappings {
    public class SessionMapping : EntityTypeConfiguration<Session> {
        public SessionMapping() {
            HasKey(x => x.Id);

            Property(x => x.DateTime)
                .IsRequired();

            Property(x => x.Price)
                .IsRequired();

            Property(x => x.ClassLimit)
                .IsRequired();

            Property(x => x.VenueType)
                .IsRequired();

            Property(x => x.Audience)
                .IsRequired();

            Property(x => x.Address)
                .IsOptional()
                .HasMaxLength(512);

            Property(x => x.Postcode)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}