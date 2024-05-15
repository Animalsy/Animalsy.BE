using FluentValidation;

namespace Animalsy.BE.Services.VendorsAPI.Validators
{
    public class NumberValidator : AbstractValidator<string>
    {
        public NumberValidator(string desc, int length)
        {
            RuleFor(x => x).NotEmpty().Must(x => int.TryParse(x, out var result))
                .WithMessage($"{desc} number is not in correct format").Length(length);
        }
    }
}
