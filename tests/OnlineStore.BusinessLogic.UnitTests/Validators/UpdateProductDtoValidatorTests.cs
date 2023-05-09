using FizzWare.NBuilder;
using FluentValidation.TestHelper;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Validators.Product;
using Xunit;

namespace OnlineStore.BusinessLogic.UnitTests.Validators
{
    public class UpdateProductDtoValidatorTests
    {
        private readonly UpdateProductDtoValidator _updateProductDtoValidator;

        public UpdateProductDtoValidatorTests()
        {
            _updateProductDtoValidator = new UpdateProductDtoValidator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validator_Should_Have_Error_When_Id_Is_Not_Positive(int id)
        {
            // Arrange
            var updateProductDto = new UpdateProductDto { Id = id };

            // Act
            var result = _updateProductDtoValidator.TestValidate(updateProductDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var updateProductDto = new UpdateProductDto { Name = string.Empty };

            // Act
            var result = _updateProductDtoValidator.TestValidate(updateProductDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok()
        {
            // Arrange
            var updateProductDto = Builder<UpdateProductDto>
                .CreateNew()
                .Build();

            // Act
            var result = _updateProductDtoValidator.TestValidate(updateProductDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
