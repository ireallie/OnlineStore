using FizzWare.NBuilder;
using FluentValidation.TestHelper;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;
using OnlineStore.BusinessLogic.Validators.ProductOption;
using Xunit;

namespace OnlineStore.BusinessLogic.UnitTests.Validators
{
    public class UpdateProductOptionDtoValidatorTests
    {
        private readonly UpdateProductOptionDtoValidator _updateProductOptionDtoValidator;
        public UpdateProductOptionDtoValidatorTests()
        {
            _updateProductOptionDtoValidator = new UpdateProductOptionDtoValidator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validator_Should_Have_Error_When_Id_Is_Not_Positive(int id)
        {
            // Arrange
            var updateProductOptionDto = new UpdateProductOptionDto { Id = id };

            // Act
            var result = _updateProductOptionDtoValidator.TestValidate(updateProductOptionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(1)]
        public void Validator_Should_Not_Have_Error_When_Id_Is_Positive(int id)
        {
            // Arrange
            var updateProductOptionDto = new UpdateProductOptionDto { Id = id };

            // Act
            var result = _updateProductOptionDtoValidator.TestValidate(updateProductOptionDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validator_Should_Have_Error_When_ProductId_Is_Not_Positive(int productId)
        {
            // Arrange
            var updateProductOptionDto = new UpdateProductOptionDto { ProductId = productId };

            // Act
            var result = _updateProductOptionDtoValidator.TestValidate(updateProductOptionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Theory]
        [InlineData(1)]
        public void Validator_Should_Not_Have_Error_When_ProductId_Is_Positive(int productId)
        {
            // Arrange
            var updateProductOptionDto = new UpdateProductOptionDto { ProductId = productId };

            // Act
            var result = _updateProductOptionDtoValidator.TestValidate(updateProductOptionDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ProductId);
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var updateProductOptionDto = new UpdateProductOptionDto { Name = string.Empty };

            // Act
            var result = _updateProductOptionDtoValidator.TestValidate(updateProductOptionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Values_Are_Empty()
        {
            // Arrange
            var updateProductOptionDto = new UpdateProductOptionDto { Values = null };

            // Act
            var result = _updateProductOptionDtoValidator.TestValidate(updateProductOptionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Values);
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Some_Value_Is_Empty()
        {
            // Arrange
            var updateProductOptionDto = new UpdateProductOptionDto
            {
                Values = new string[] { string.Empty }
            };

            // Act
            var result = _updateProductOptionDtoValidator.TestValidate(updateProductOptionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Values);
        }

        [Fact]
        public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok()
        {
            // Arrange
            var updateProductOptionDto = Builder<UpdateProductOptionDto>
                .CreateNew()
                .Build();

            updateProductOptionDto.Values.Add("values");

            // Act
            var result = _updateProductOptionDtoValidator.TestValidate(updateProductOptionDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Values);
        }
    }
}
