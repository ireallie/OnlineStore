using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Data.Configurations
{
    public class OptionValueConfiguration
         : IEntityTypeConfiguration<OptionValue>
    {
        public void Configure(EntityTypeBuilder<OptionValue> builder)
        {
            builder.HasIndex(ov => new { ov.Value, ov.OptionId, })
                .IsUnique();

            builder.Property(ov => ov.Value)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
