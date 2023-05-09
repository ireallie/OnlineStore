using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Data.Configurations
{
    public class VariantConfiguration
        : IEntityTypeConfiguration<Variant>
    {
        public void Configure(EntityTypeBuilder<Variant> builder)
        {
            builder.Property(v => v.SKU)
                .HasMaxLength(50);

            builder.Property(v => v.RegularPrice)
                .HasPrecision(18, 4);

            builder.Property(v => v.SalePrice)
                .HasPrecision(18, 4);
        }
    }
}
