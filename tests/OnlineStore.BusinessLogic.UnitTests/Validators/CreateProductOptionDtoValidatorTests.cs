using FizzWare.NBuilder;
using FluentValidation.TestHelper;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;
using OnlineStore.BusinessLogic.Validators.ProductOption;
using Xunit;

namespace OnlineStore.BusinessLogic.UnitTests.Validators
{
    public class CreateProductOptionDtoValidatorTests
    {
        private readonly CreateProductOptionDtoValidator _createProductOptionDtoValidator;
        public CreateProductOptionDtoValidatorTests()
        {
            _createProductOptionDtoValidator = new CreateProductOptionDtoValidator();
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var createProductOptionDto = new CreateProductOptionDto { Name = string.Empty };

            // Act
            var result = _createProductOptionDtoValidator.TestValidate(createProductOptionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Values_Are_Empty()
        {
            // Arrange
            var createProductOptionDto = new CreateProductOptionDto { Values = null };

            // Act
            var result = _createProductOptionDtoValidator.TestValidate(createProductOptionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Values);
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Some_Value_Is_Empty()
        {
            // Arrange
            var createProductOptionDto = new CreateProductOptionDto
            {
                Values = new string[] { string.Empty }
            };

            // Act
            var result = _createProductOptionDtoValidator.TestValidate(createProductOptionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Values);
        }

        [Fact]
        public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok()
        {
            // Arrange
            var createProductOptionDto = Builder<CreateProductOptionDto>
                .CreateNew()
                .Build();

            createProductOptionDto.Values.Add("values");

            // Act
            var result = _createProductOptionDtoValidator.TestValidate(createProductOptionDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Values);
        }
    }
}
