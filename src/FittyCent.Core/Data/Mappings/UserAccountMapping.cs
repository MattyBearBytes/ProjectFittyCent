using System.Data.Entity.ModelConfiguration;
using FittyCent.Domain;

namespace FittyCent.Data.Mappings {
    public class UserAccountMapping : EntityTypeConfiguration<UserAccount> {
        public UserAccountMapping() {
            ToTable("UserAccounts");

            HasKey(x => x.Id);

            Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(512);

            Property(x => x.UserType)
                .IsRequired();

            Property(x => x.FirstName)
                .IsOptional()
                .HasMaxLength(255);

            Property(x => x.Surname)
                .IsOptional()
                .HasMaxLength(255);

            Property(x => x.Postcode)
                .IsOptional()
                .HasMaxLength(20);

            HasMany(x => x.Classes)
                .WithRequired(x => x.User);
        }
    }
}