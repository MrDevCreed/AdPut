using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(P => P.UserId).IsRequired();
            builder.Property(P => P.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(P => P.UserId);
        }
    }
}
