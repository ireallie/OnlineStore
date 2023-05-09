using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contracts.Entities.Products;
using Xunit;

namespace OnlineStore.Data.UnitTests.Repositories
{
    public class ProductRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _databaseFixture;

        public ProductRepositoryTests(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
        }

        [Fact]
        public async Task GetListAsync_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            // Act
            var products = await repository.GetListAsync();

            // Assert
            Assert.NotNull(products);
            Assert.Equal(3, products.Count);
        }

        [Fact]
        public async Task GetFullProductByIdAsync_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            // Act
            var product = await repository.GetProductWithDetailsAsync(1);

            // Assert
            Assert.NotNull(product);

            Assert.Equal("ProductName1", product.Name);

            Assert.NotNull(product.Options);
            Assert.Equal(1, product.Options.Count);
            Assert.Equal(2, product.Options.SelectMany(x => x.OptionValues).Count());

            Assert.NotNull(product.Variants);
            Assert.Equal(2, product.Variants.Count);
            Assert.Equal(2, product.Variants.SelectMany(x => x.VariantOptionValues).Count());

            Assert.True(product.Variants
                .All(x => x.VariantOptionValues
                    .All(x => x.OptionValue != null && x.OptionValue.Option != null)));
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            var optionValues = Builder<OptionValue>
                .CreateListOfSize(2)
                .All()
                .With(o => o.OptionValueId = 0)
                .Build();

            var option = Builder<Option>
                .CreateNew()
                .WithFactory(() => new Option(optionValues))
                .With(o => o.OptionId = 0)
                .Build();

            var variantOptionValues = Builder<VariantOptionValue>
                .CreateListOfSize(2)
                .All()
                .With(v => v.VariantId = 0)
                .Build();

            var variants = Builder<Variant>
                .CreateListOfSize(2)
                .All()
                .With(v => v.VariantId = 0)
                .Build();

            variants[0].VariantOptionValues.Add(variantOptionValues[0]);
            variants[1].VariantOptionValues.Add(variantOptionValues[1]);

            var product = Builder<Product>
                .CreateNew()
                .WithFactory(() => new Product(new List<Option>() { option }, variants))
                .With(p => p.ProductId = 0)
                .Build();

            // Act
            await repository.AddAsync(product);

            var rowsAffected = await uow.SaveChangesAsync();
            var productsCount = await repository.CountAsync();

            // Assert
            Assert.Equal(4, productsCount);
            Assert.Equal(8, rowsAffected);
        }

        [Fact]
        public async Task AddAsync_Failure()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            var product = Builder<Product>
                .CreateNew()
                .With(p => p.ProductId = 0)
                .With(p => p.Name = null)
                .Build();

            // Act
            await repository.AddAsync(product);
            var productsCount = await repository.CountAsync();

            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => uow.SaveChangesAsync());
            Assert.Equal(3, productsCount);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;
            var productToDelete = await repository.GetByIdAsync(1);

            // Act
            repository.Delete(productToDelete);
            var rowsAffected = await uow.SaveChangesAsync();
            var productsCount = await repository.CountAsync();

            // Assert
            Assert.Equal(2, productsCount);
            Assert.Equal(8, rowsAffected);
        }

        [Fact]
        public async Task UpdateAsync_Updates_Specific_Fields_In_Product_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            var product = Builder<Product>
                .CreateNew()
                .With(p => p.ProductId = 1)
                .With(p => p.Name = "Name Something new")
                .With(p => p.Description = "Description Something new")
                .With(p => p.Label = "Label Something new")
                .With(p => p.IsVisible = false)
                .Build();

            // Act
            var updatedProduct = repository.Update(product);
            await uow.SaveChangesAsync();

            // Assert
            Assert.Equal(product.Name, updatedProduct.Name);
            Assert.Equal(product.Description, updatedProduct.Description);
            Assert.Equal(product.Label, updatedProduct.Label);
            Assert.Equal(product.IsVisible, updatedProduct.IsVisible);
        }

        [Fact]
        public async Task UpdateAsync_Updates_Product_And_One_Of_Its_Variants_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            var variant = Builder<Variant>
                .CreateNew()
                .With(v => v.VariantId = 1)
                .With(v => v.RegularPrice = 2000)
                .With(v => v.SKU = "Updating the Product SKU")
                .Build();

            var product = Builder<Product>
                .CreateNew()
                .WithFactory(() => new Product(new List<Option>(), new List<Variant>() { variant }))
                .With(p => p.ProductId = 1)
                .With(p => p.Name = "Updated Product Name")
                .Build();

            // Act
            var updatedProduct = repository.Update(product);
            await uow.SaveChangesAsync();

            // Assert
            Assert.Equal(product.Name, updatedProduct.Name);
            Assert.Equal(product.Variants.Count, updatedProduct.Variants.Count);
            Assert.Equal(product.Variants.First().VariantId, updatedProduct.Variants.First().VariantId);
            Assert.Equal(product.Variants.First().RegularPrice, updatedProduct.Variants.First().RegularPrice);
            Assert.Equal(product.Variants.First().SKU, updatedProduct.Variants.First().SKU);
        }

        [Fact]
        public async Task UpdateAsync_Updates_Product_And_One_Of_Its_Options_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            var option = Builder<Option>
                .CreateNew()
                .With(o => o.OptionId = 1)
                .With(o => o.Name = "Updated Option Name")
                .Build();

            var product = Builder<Product>
                .CreateNew()
                .WithFactory(() => new Product(new List<Option>() { option }, new List<Variant>()))
                .With(p => p.ProductId = 1)
                .With(p => p.Name = "Updated Product Name")
                .Build();

            // Act
            var updatedProduct = repository.Update(product);
            await uow.SaveChangesAsync();

            // Assert
            Assert.Equal(product.Name, updatedProduct.Name);
            Assert.Equal(product.Options.Count, updatedProduct.Options.Count);
            Assert.Equal(product.Options.First().OptionId, updatedProduct.Options.First().OptionId);
            Assert.Equal(product.Options.First().Name, updatedProduct.Options.First().Name);
        }

        [Fact]
        public async Task UpdateAsync_Updates_Product_By_Clearing_Options_And_Variants_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            var product = Builder<Product>
                .CreateNew()
                .WithFactory(() => new Product(new List<Option>() { }, new List<Variant>()))
                .With(p => p.ProductId = 1)
                .With(p => p.Name = "Updated Product Name")
                .Build();

            // Act
            var updatedProduct = repository.Update(product);
            var rowsAffected = await uow.SaveChangesAsync();

            // Assert
            Assert.Equal(8, rowsAffected);
            Assert.Equal(product.Name, updatedProduct.Name);
            Assert.Equal(0, updatedProduct.Options.Count);
            Assert.Equal(0, updatedProduct.Variants.Count);
        }

        [Fact]
        public async Task UpdateAsync_Updates_Product_By_Adding_A_New_Option_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            var newOptionValue = Builder<OptionValue>
               .CreateNew()
               .With(o => o.OptionValueId = 0)
               .Build();

            var newOption = Builder<Option>
               .CreateNew()
               .WithFactory(() => new Option(new List<OptionValue>() { newOptionValue }))
               .With(o => o.OptionId = 0)
               .Build();

            var productToUpdate = await repository.GetProductWithDetailsAsync(1);
            productToUpdate.Options.Add(newOption);

            foreach (var existingVariant in productToUpdate.Variants)
            {
                existingVariant.VariantOptionValues.Add(new VariantOptionValue { OptionValue = newOptionValue });
            }

            // Act
            var updatedProduct = repository.Update(productToUpdate);
            await uow.SaveChangesAsync();

            // Assert
            Assert.Equal(2, updatedProduct.Options.Count);
            Assert.Equal(3, updatedProduct.Options.SelectMany(x => x.OptionValues).Count());
            Assert.Equal(2, updatedProduct.Variants.Count);
            Assert.Equal(4, updatedProduct.Variants.SelectMany(x => x.VariantOptionValues).Count());
        }

        [Fact]
        public async Task UpdateAsync_Updates_Product_By_Deleting_An_Existing_OptionValue_Success()
        {
            // Arrange
            var uow = await _databaseFixture.CreateUnitOfWork();
            var repository = uow.ProductRepository;

            var productToUpdate = await repository.GetProductWithDetailsAsync(1);
            var option = productToUpdate.Options.First();

            option.OptionValues.Remove(option.OptionValues.First());
            productToUpdate.Variants.Remove(productToUpdate.Variants.First());

            // Act
            Product updatedProduct = repository.Update(productToUpdate);
            await uow.SaveChangesAsync();

            // Assert
            Assert.Equal(1, updatedProduct.Options.Count);
            Assert.Single(updatedProduct.Options.SelectMany(x => x.OptionValues));
            Assert.Equal(1, updatedProduct.Variants.Count);
            Assert.Single(updatedProduct.Variants.SelectMany(x => x.VariantOptionValues));
        }
    }
}
