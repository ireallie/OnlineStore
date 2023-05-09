using FluentValidation;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;

namespace OnlineStore.BusinessLogic.Validators.ProductOption
{
    public class UpdateProductOptionDtoValidator : AbstractValidator<UpdateProductOptionDto>
    {
        public UpdateProductOptionDtoValidator()
        {
            RuleFor(x => x.Id)
                .InclusiveBetween(0, int.MaxValue); // Can be 0 when child entity is being added when updating parent entity

            RuleFor(x => x.ProductId)
                .InclusiveBetween(1, int.MaxValue);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ProductOptionDtoConfiguration.MaximumNameLength);

            RuleFor(x => x.Values)
                .NotEmpty();

            RuleForEach(x => x.Values)
                .NotEmpty()
                .MaximumLength(ProductOptionDtoConfiguration.MaximumValueLength);
        }
    }
}
