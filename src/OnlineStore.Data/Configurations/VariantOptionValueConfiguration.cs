using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Data.Configurations
{
    public class VariantOptionValueConfiguration
        : IEntityTypeConfiguration<VariantOptionValue>
    {
        public void Configure(EntityTypeBuilder<VariantOptionValue> builder)
        {
            builder.HasKey(vov => new { vov.VariantId, vov.OptionValueId });

            builder.HasOne(vov => vov.Variant)
                .WithMany(v => v.VariantOptionValues)
                .HasForeignKey(vov => vov.VariantId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(vov => vov.OptionValue)
                .WithMany(ov => ov.VariantOptionValues)
                .HasForeignKey(vov => vov.OptionValueId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
