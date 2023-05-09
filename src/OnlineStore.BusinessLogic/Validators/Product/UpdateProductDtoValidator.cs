using FluentValidation;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;

namespace OnlineStore.BusinessLogic.Validators.Product
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Id)
                .InclusiveBetween(1, int.MaxValue);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ProductDtoValidatorConfiguration.MaximumNameLength);

            RuleFor(x => x.Description)
                .MaximumLength(ProductDtoValidatorConfiguration.MaximumDescriptionLength);

            RuleFor(x => x.Label)
                .MaximumLength(ProductDtoValidatorConfiguration.MaximumLabelLength);

            RuleFor(x => x.SKU)
                .MaximumLength(ProductDtoValidatorConfiguration.MaximumSKULength);

            RuleFor(x => x.RegularPrice)
                .InclusiveBetween(0, int.MaxValue);

            RuleFor(x => x.SalePrice)
                .InclusiveBetween(0, int.MaxValue);
        } 
    }
}
