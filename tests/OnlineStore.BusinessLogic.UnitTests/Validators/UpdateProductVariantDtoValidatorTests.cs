using FluentValidation.TestHelper;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant;
using OnlineStore.BusinessLogic.Validators.ProductOption;
using OnlineStore.BusinessLogic.Validators.ProductVariant;
using Xunit;

namespace OnlineStore.BusinessLogic.UnitTests.Validators
{
    public class UpdateProductVariantDtoValidatorTests
    {
        private readonly UpdateProductVariantDtoValidator _updateProductVariantDtoValidator;
        public UpdateProductVariantDtoValidatorTests()
        {
            _updateProductVariantDtoValidator = new UpdateProductVariantDtoValidator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validator_Should_Have_Error_When_Id_Is_Not_Positive(int id)
        {
            // Arrange
            var updateProductVariantDto = new UpdateProductVariantDto { Id = id };

            // Act
            var result = _updateProductVariantDtoValidator.TestValidate(updateProductVariantDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(1)]
        public void Validator_Should_Not_Have_Error_When_Id_Is_Positive(int id)
        {
            // Arrange
            var updateProductVariantDto = new UpdateProductVariantDto { Id = id };

            // Act
            var result = _updateProductVariantDtoValidator.TestValidate(updateProductVariantDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validator_Should_Have_Error_When_ProductId_Is_Not_Positive(int productId)
        {
            // Arrange
            var updateProductVariantDto = new UpdateProductVariantDto { ProductId = productId };

            // Act
            var result = _updateProductVariantDtoValidator.TestValidate(updateProductVariantDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Theory]
        [InlineData(1)]
        public void Validator_Should_Not_Have_Error_When_ProductId_Is_Positive(int productId)
        {
            // Arrange
            var updateProductVariantDto = new UpdateProductVariantDto { ProductId = productId };

            // Act
            var result = _updateProductVariantDtoValidator.TestValidate(updateProductVariantDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ProductId);
        }
    }
}
