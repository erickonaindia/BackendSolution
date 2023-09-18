using FluentValidation;
using Solution.Application.Features.Categories.DTOs;

namespace Solution.Application.Features.Categories.Validators
{
    public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");

            RuleFor(p => p.Products).Empty();
                
        }
    }
}
