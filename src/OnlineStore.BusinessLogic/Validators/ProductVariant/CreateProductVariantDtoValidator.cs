using FluentValidation;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant;

namespace OnlineStore.BusinessLogic.Validators.ProductVariant
{
    public class CreateProductVariantDtoValidator : AbstractValidator<CreateProductVariantDto>
    {
        public CreateProductVariantDtoValidator()
        {
            RuleFor(x => x.SKU)
              .MaximumLength(ProductVariantDtoConfiguration.MaximumSKULength);

            RuleFor(x => x.RegularPrice)
                .InclusiveBetween(0, int.MaxValue);

            RuleFor(x => x.SalePrice)
                .InclusiveBetween(0, int.MaxValue)
                .When(x => x.SalePrice.HasValue);
        } 
    }
}
