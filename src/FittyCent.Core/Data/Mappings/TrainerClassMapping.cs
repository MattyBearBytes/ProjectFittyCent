using System.Data.Entity.ModelConfiguration;
using FittyCent.Domain;

namespace FittyCent.Data.Mappings {
    public class TrainerClassMapping : EntityTypeConfiguration<TrainerClass> {
        public TrainerClassMapping() {
            HasKey(x => x.Id);

            Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(512);

            Property(x => x.Summary)
                .IsOptional()
                .HasMaxLength(5000);

            Property(x => x.Type)
                .IsRequired();

            Property(x => x.Keywords)
                .IsRequired()
                .IsMaxLength();
        }
    }
}