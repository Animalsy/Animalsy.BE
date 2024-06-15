using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.CustomerAPI.Validators;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
{
    internal static string InvalidEmailAddressMessage = "Email address is not in correct format";
    internal static string InvalidPhoneNumberMessage = "Phone number is not in correct format";
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.City).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Street).NotEmpty().MaximumLength(40);
        RuleFor(x => x.Building).NotEmpty().MaximumLength(5);
        RuleFor(x => x.PostalCode).NotEmpty().Length(6);
        RuleFor(x => x.Flat).MaximumLength(5).When(x => x.Flat is not null);
        RuleFor(x => x.EmailAddress).NotEmpty()
            .EmailAddress().WithMessage(InvalidEmailAddressMessage)
            .MaximumLength(50);
        RuleFor(x => x.PhoneNumber).NotEmpty()
            .Must(x => int.TryParse(x, out var result)).WithMessage(InvalidPhoneNumberMessage)
            .Length(9);
    }
}