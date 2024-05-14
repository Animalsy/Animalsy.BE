using FluentValidation;

namespace Animalsy.BE.Services.CustomersAPI.Validators
{
    public class PhoneNumberValidator : AbstractValidator<string>
    {
        internal static string InvalidPhoneNumberMessage = "Phone number is not in correct format";
        public PhoneNumberValidator()
        {
            RuleFor(x => x).NotEmpty().Must(x => int.TryParse(x, out var result)).WithMessage(InvalidPhoneNumberMessage).Length(9);
        }
    }
}
