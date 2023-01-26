using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.EntityConfigurations
{
    public class AdEntityTypeConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(100).IsRequired();
            builder.Property(P => P.Price).IsRequired();
            builder.Property(P => P.Description).HasMaxLength(250).IsRequired();
            builder.HasIndex(P => P.Name);
            builder.HasIndex(P => P.Price);
            builder.HasOne(a => a.Address).WithOne(b => b.Ad).HasForeignKey<Address>(P => P.AdId);
        }
    }
}
