using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.EntityConfigurations
{
    public class ImageEntityTypeConfigurtion : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(P => P.ImagePath).IsRequired();
        }
    }
}
