using FluentValidation;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductVariant;

namespace OnlineStore.BusinessLogic.Validators.ProductVariant
{
    public class UpdateProductVariantDtoValidator : AbstractValidator<UpdateProductVariantDto>
    {
        public UpdateProductVariantDtoValidator()
        {
            RuleFor(x => x.Id)
                .InclusiveBetween(0, int.MaxValue); // Can be 0 when child entity is being added when updating parent entity

            RuleFor(x => x.ProductId)
                .InclusiveBetween(1, int.MaxValue);

            RuleFor(x => x.SKU)
              .MaximumLength(ProductVariantDtoConfiguration.MaximumSKULength);

            RuleFor(x => x.RegularPrice)
                .InclusiveBetween(0, int.MaxValue);

            RuleFor(x => x.SalePrice)
                .InclusiveBetween(0, int.MaxValue);
        }
    }
}
