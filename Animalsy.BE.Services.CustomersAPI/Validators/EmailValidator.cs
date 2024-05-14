using FluentValidation;

namespace Animalsy.BE.Services.CustomersAPI.Validators
{
    public class EmailValidator : AbstractValidator<string>
    {
        internal static string InvalidEmailAddressMessage = "Email address is not in correct format";
        public EmailValidator()
        {
            RuleFor(x => x).NotEmpty().EmailAddress().WithMessage(InvalidEmailAddressMessage).Length(1, 50);
        }
    }
}
