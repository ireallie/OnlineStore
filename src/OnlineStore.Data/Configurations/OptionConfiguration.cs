using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Data.Configurations
{
    public class OptionConfiguration
        : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.Property(o => o.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
