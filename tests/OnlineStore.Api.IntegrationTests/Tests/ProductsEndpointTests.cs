using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OnlineStore.Api.IntegrationTests.Config;
using OnlineStore.Api.IntegrationTests.Helpers;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.Data;
using OnlineStore.Data.Contracts.Entities.Products;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineStore.Api.IntegrationTests.Tests
{
    [TestFixture]
    public class ProductsEndpointTests : BaseTestFixture
    {
        [Test]
        public async Task Create_Should_Add_Product_To_Database()
        {
            // Arrange
            var client = _factory.CreateClient();

            var createProductDto = Builder<CreateProductDto>
                .CreateNew()
                .Build();

            // Act  
            var response = await client.PostAsync("/api/products", new JsonContent(createProductDto));
            var result = await ResponseHelper.GetApiResultAsync<ProductDto>(response);
            var product = await _context.Products
                .Where(p => p.ProductId == result.Data.Id)
                .FirstOrDefaultAsync();

            // Assert
            CheckResponse.Succeeded(result);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            product.Should().NotBeNull();
            product.Name.Should().Be(createProductDto.Name);
        }

        [Test]
        public async Task Create_Should_Return_BadRequest_If_ProductName_Is_Empty()
        {
            // Arrange
            var client = _factory.CreateClient();

            var createProductDto = Builder<CreateProductDto>
                .CreateNew()
                .With(x => x.Name = string.Empty)
                .Build();

            // Act
            var response = await client.PostAsync("/api/products", new JsonContent(createProductDto));
            var result = await ResponseHelper.GetApiResultAsync<ProductDto>(response);
            var createdProduct = await _context.Products
                .Where(p => p.Name == createProductDto.Name)
                .FirstOrDefaultAsync();

            // Assert
            CheckResponse.Failure(result);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            createdProduct.Should().BeNull();
        }

        [Test]
        public async Task Update_Should_Update_Product_In_Database()
        {
            // Arrange
            var client = _factory.CreateClient();

            var product = Builder<Product>
                .CreateNew()
                .Build();

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var updateProductDto = Builder<UpdateProductDto>
                .CreateNew()
                .With(x => x.Id == product.ProductId)
                .With(x => x.Name = "UpdateProductDtoName")
                .Build();

            // Act
            var scope = _factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            _context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var response = await client.PutAsync("/api/products", new JsonContent(updateProductDto));
            var result = await ResponseHelper.GetApiResultAsync<ProductDto>(response);
            var updatedProduct = await _context.Products
                .Where(p => p.ProductId == result.Data.Id)
                .FirstOrDefaultAsync();

            // Assert
            CheckResponse.Succeeded(result);

            updatedProduct.Should().NotBeNull();
            updatedProduct.Name.Should().Be(updateProductDto.Name);
        }

        [Test]
        public async Task Update_Should_Return_NotFound_If_Product_Does_Not_Exist_Anymore()
        {
            // Arrange
            var client = _factory.CreateClient();

            var updateProductDto = Builder<UpdateProductDto>
                .CreateNew()
                .With(x => x.Name = "UpdateProductDtoName")
                .Build();

            // Act
            var response = await client.PutAsync("/api/products", new JsonContent(updateProductDto));
            var result = await ResponseHelper.GetApiResultAsync<ProductDto>(response);
            var updatedProduct = await _context.Products
                .Where(p => p.Name == updateProductDto.Name)
                .FirstOrDefaultAsync();

            // Assert
            CheckResponse.Failure(result);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            updatedProduct.Should().BeNull();
        }

        [Test]
        public async Task Delete_Should_Delete_Product_From_Database()
        {
            // Arrange
            var client = _factory.CreateClient();

            var product = Builder<Product>
                .CreateNew()
                .Build();

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            var response = await client.DeleteAsync($"/api/products/{product.ProductId}");
            var result = await ResponseHelper.GetApiResultAsync<string>(response);
            var deletetedProduct = await _context.Products
                .Where(p => p.ProductId == product.ProductId)
                .FirstOrDefaultAsync();

            // Assert
            CheckResponse.Succeeded(result);

            deletetedProduct.Should().BeNull();
        }

        [Test]
        public async Task Delete_Should_Return_NotFound_If_Product_Does_Not_Exist_Anymore()
        {
            // Arrange
            var client = _factory.CreateClient();
            var randomId = 99999;

            // Act
            var response = await client.DeleteAsync($"/api/products/{randomId}");
            var result = await ResponseHelper.GetApiResultAsync<string>(response);

            // Assert
            CheckResponse.Failure(result);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetAll_Should_Return_All_Products_From_Database()
        {
            // Arrange
            var client = _factory.CreateClient();

            var products = Builder<Product>
                .CreateListOfSize(10)
                .All()
                .Build();   

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/api/products");
            var result = await ResponseHelper.GetApiResultAsync<IEnumerable<ProductDto>>(response);

            // Assert
            CheckResponse.Succeeded(result);

            result.Data.Should().HaveCount(10);
        }
    }
}
