using System.Data.Entity.ModelConfiguration;
using FittyCent.Domain;

namespace FittyCent.Data.Mappings {
    public class UserAccountMapping : EntityTypeConfiguration<UserAccount> {
        public UserAccountMapping() {
            ToTable("UserAccounts");

            HasKey(x => x.Id);

            Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}