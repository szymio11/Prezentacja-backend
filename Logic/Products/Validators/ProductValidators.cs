using System.Data;
using Api.NetCore.Domains;
using FluentValidation;
using FluentValidation.Validators;

namespace Logic.Products.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Nazwa nie może być pusta");

            RuleFor(p=>p.Description)
                .NotEmpty()
                .WithMessage("Opis nie może być pusty");

            RuleFor(p => p.Category)
                .NotNull()
                .WithMessage("Kategoria nie może być pusta");

            RuleFor(p=>p.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Cena nie może być pusta")
                .SetValidator(new ScalePrecisionValidator(2,9))
                .GreaterThan(0)
                .WithMessage("Cena musi być większa niż 0");




        }
    }
}