using FluentValidation.TestHelper;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Validators.Product;
using Xunit;

namespace OnlineStore.BusinessLogic.UnitTests.Validators
{
    public class CreateProductDtoValidatorTests
    {
        private readonly CreateProductDtoValidator _createProductDtoValidator;

        public CreateProductDtoValidatorTests()
        {
            _createProductDtoValidator = new CreateProductDtoValidator();
        }

        [Theory]
        [InlineData("")]
        public void Validate_WhenNameIsEmpty_ThrowsValidationError(string name)
        {
            var createProductDto = new CreateProductDto()
            {
                Name = name,
                Description = "",
                SKU = "",
                RegularPrice = 0,
                SalePrice = null,
                Label = "",
                IsVisible = false,
                Quantity = null
            };

            var result = _createProductDtoValidator.TestValidate(createProductDto);
            result.ShouldHaveAnyValidationError();
        }

        [Fact]
        public void Validate_WhenNameIsNotEmpty_ShouldNotThrowValidationError()
        {
            var createProductDto = new CreateProductDto()
            {
                Name = "name",
                Description = "",
                SKU = "",
                RegularPrice = 0,
                SalePrice = null,
                Label = "",
                IsVisible = false,
                Quantity = null
            };

            var result = _createProductDtoValidator.TestValidate(createProductDto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(
            "Lorem ipsum dolor sit amet, consectetuer adipiscing", 
            "Lorem ipsum dolor sit amet, consectetuer adipiscing",
            "Lorem ipsum dolor sit amet, consectetuer adipiscing"
        )]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing", "L", "L")]
        [InlineData("L", "Lorem ipsum dolor sit amet, consectetuer adipiscing","L")]
        [InlineData("L","L","Lorem ipsum dolor sit amet, consectetuer adipiscing")]
        public void Validate_WhenOneOfTheFieldsOrAllFieldsExceedMaximumLengthOf50_ThrowsValidationError
            (string name, string label, string SKU)
        {
            var createProductDto = new CreateProductDto()
            {
                Name = name,
                Description = "",
                SKU = SKU,
                RegularPrice = 0,
                SalePrice = null,
                Label = label,
                IsVisible = false,
                Quantity = null
            };

            var result = _createProductDtoValidator.TestValidate(createProductDto);
            result.ShouldHaveAnyValidationError();
        }

        [Fact]
        public void Validate_WhenNameAndLabelAndSKU_DoesNotExceedMaximumLengthOf50_ShouldNotHaveAnyValidationErrors()
        {
            var createProductDto = new CreateProductDto()
            {
                Name = "name",
                Description = "",
                SKU = "SKU",
                RegularPrice = 0,
                SalePrice = null,
                Label = "label",
                IsVisible = false,
                Quantity = null
            };

            var result = _createProductDtoValidator.TestValidate(createProductDto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_WhenDescription_DoesNotExceedMaximumLengthOf500_ShouldNotHaveAnyValidationErrors()
        {
            var createProductDto = new CreateProductDto()
            {
                Name = "name",
                Description = "description",
                SKU = "",
                RegularPrice = 0,
                SalePrice = null,
                Label = "",
                IsVisible = false,
                Quantity = null
            };

            var result = _createProductDtoValidator.TestValidate(createProductDto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_WhenDescriptionExceedsMaximumLengthOf500_ThrowsValidationError()
        {
            var createProductDto = new CreateProductDto()
            {
                Name = "name",
                Description = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. 
                                Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.
                                Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem.
                                Nulla consequat massa quis enim. 
                                Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. 
                                In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. 
                                Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibua",
                SKU = "",
                RegularPrice = 0,
                SalePrice = null,
                Label = "",
                IsVisible = false,
                Quantity = null
            };

            var result = _createProductDtoValidator.TestValidate(createProductDto);
            result.ShouldHaveAnyValidationError();
        }
    }
}
