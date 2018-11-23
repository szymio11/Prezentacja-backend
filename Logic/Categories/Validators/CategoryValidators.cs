using Api.NetCore.Domains;
using FluentValidation;

namespace Logic.Categories.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Nazwa nie może być pusta");
        }
    }
}