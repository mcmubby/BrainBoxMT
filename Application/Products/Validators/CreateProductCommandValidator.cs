using FluentValidation;

namespace Application.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Quantity).GreaterThan(0);
        }
    }
}