using System.Data.Entity.ModelConfiguration;
using FittyCent.Domain;

namespace FittyCent.Data.Mappings {
    public class TrainerProfileMapping : ComplexTypeConfiguration<TrainerProfile> {
        public TrainerProfileMapping() {
            Property(x => x.Summary)
                .IsOptional()
                .HasMaxLength(5000);

            Property(x => x.CompanyName)
                .IsOptional()
                .HasMaxLength(255);

            Property(x => x.CompanyWebsite)
                .IsOptional()
                .HasMaxLength(255);

            Property(x => x.IsInsured)
                .IsOptional();

            Property(x => x.Registrations)
                .IsOptional()
                .IsMaxLength();

            Property(x => x.Specialisations)
                .IsOptional()
                .IsMaxLength();

            Property(x => x.Qualifications)
                .IsOptional()
                .IsMaxLength();

            Property(x => x.HasMobileServiceAvailable)
                .IsOptional();
        }
    }
}