using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contracts.Entities.Auditing;
using OnlineStore.Data.Contracts.Entities.Products;
using System.Linq.Expressions;

namespace OnlineStore.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "t-shirt the hate",
                    Description = "T-shirt the hate,100 cotton, machine embroidery, oversized fit.\r\n\r\nso much hate for me everywhere today, can't figure out what i did to cause it... i am trying to ovrercome, to overcome it all.",
                    Label = "preorder",
                    IsVisible = true,
                    CreatedDate = DateTimeOffset.UtcNow,
                }
            );

            modelBuilder.Entity<Option>().HasData(
                new Option { OptionId = 1, Name = "Size", ProductId = 1, CreatedDate = DateTimeOffset.UtcNow },
                new Option { OptionId = 2, Name = "Color", ProductId = 1, CreatedDate = DateTimeOffset.UtcNow }
            );

            modelBuilder.Entity<OptionValue>().HasData(
                new OptionValue { OptionValueId = 1, Value = "s-m", OptionId = 1, CreatedDate = DateTimeOffset.UtcNow }, 
                new OptionValue { OptionValueId = 2, Value = "m-l", OptionId = 1, CreatedDate = DateTimeOffset.UtcNow },
                new OptionValue { OptionValueId = 3, Value = "xl+", OptionId = 1, CreatedDate = DateTimeOffset.UtcNow },
                new OptionValue { OptionValueId = 4, Value = "black", OptionId = 2, CreatedDate = DateTimeOffset.UtcNow },
                new OptionValue { OptionValueId = 5, Value = "white", OptionId = 2, CreatedDate = DateTimeOffset.UtcNow }
            );

            modelBuilder.Entity<Variant>().HasData(
                new Variant { VariantId = 1, ProductId = 1, SKU = "TTHS-MCB", RegularPrice = 3000, CreatedDate = DateTimeOffset.UtcNow },
                new Variant { VariantId = 2, ProductId = 1, SKU = "TTHS-MCW", RegularPrice = 3000, CreatedDate = DateTimeOffset.UtcNow },
                new Variant { VariantId = 3, ProductId = 1, SKU = "TTHM-LCB", RegularPrice = 3000, CreatedDate = DateTimeOffset.UtcNow },
                new Variant { VariantId = 4, ProductId = 1, SKU = "TTHM-LCW", RegularPrice = 3000, CreatedDate = DateTimeOffset.UtcNow },
                new Variant { VariantId = 5, ProductId = 1, SKU = "TTHXL+CB", RegularPrice = 3000, CreatedDate = DateTimeOffset.UtcNow },
                new Variant { VariantId = 6, ProductId = 1, SKU = "TTHXL+CW", RegularPrice = 3000, CreatedDate = DateTimeOffset.UtcNow }
            );

            modelBuilder.Entity<VariantOptionValue>().HasData(
                new VariantOptionValue { VariantId = 1, OptionValueId = 1 },
                new VariantOptionValue { VariantId = 1, OptionValueId = 4 },

                new VariantOptionValue { VariantId = 2, OptionValueId = 1 },
                new VariantOptionValue { VariantId = 2, OptionValueId = 5 },

                new VariantOptionValue { VariantId = 3, OptionValueId = 2 },
                new VariantOptionValue { VariantId = 3, OptionValueId = 4 },

                new VariantOptionValue { VariantId = 4, OptionValueId = 2 },
                new VariantOptionValue { VariantId = 4, OptionValueId = 5 },

                new VariantOptionValue { VariantId = 5, OptionValueId = 3 },
                new VariantOptionValue { VariantId = 5, OptionValueId = 4 },

                new VariantOptionValue { VariantId = 6, OptionValueId = 3 },
                new VariantOptionValue { VariantId = 6, OptionValueId = 5 }
            );
        }

        public static void HandleSoftDeletes(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IHasDeletedDate).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "p");
                    var deletedCheck = Expression.Lambda(Expression.Equal(Expression.Property(parameter, "DeletedDate"), Expression.Constant(null)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
                }
            }
        }
    }
}
