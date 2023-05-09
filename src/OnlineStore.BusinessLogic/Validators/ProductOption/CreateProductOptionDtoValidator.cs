using FluentValidation;
using OnlineStore.BusinessLogic.Contracts.Dtos.ProductOption;

namespace OnlineStore.BusinessLogic.Validators.ProductOption
{
    public class CreateProductOptionDtoValidator : AbstractValidator<CreateProductOptionDto>
    {
        public CreateProductOptionDtoValidator()
        {
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
