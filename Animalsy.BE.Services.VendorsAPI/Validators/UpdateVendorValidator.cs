using Animalsy.BE.Services.VendorAPI.Models.Dto;
using FluentValidation;
using IValidatorFactory = Animalsy.BE.Services.VendorAPI.Validators.Factory.IValidatorFactory;

namespace Animalsy.BE.Services.VendorAPI.Validators;

public class UpdateVendorValidator : AbstractValidator<UpdateVendorDto>
{
    internal static string InvalidEmailAddressMessage = "Email address is not in correct format";
    internal static string InvalidNumberMessage(string topic) => $"{topic} number is not in correct format";

    public UpdateVendorValidator(IValidatorFactory validatorFactory)
    {
        var factory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));

        RuleFor(x => x.Id).SetValidator(factory.GetValidator<Guid>());
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.City).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Street).NotEmpty().MaximumLength(40);
        RuleFor(x => x.Building).NotEmpty().MaximumLength(5);
        RuleFor(x => x.PostalCode).NotEmpty().Length(6);
        RuleFor(x => x.Flat).MaximumLength(5).When(x => x.Flat is not null);
        RuleFor(x => x.OpeningHour).NotEmpty();
        RuleFor(x => x.ClosingHour).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty()
            .EmailAddress().WithMessage(InvalidEmailAddressMessage)
            .MaximumLength(50);
        RuleFor(x => x.Nip).NotEmpty()
            .Must(x => int.TryParse(x, out var result)).WithMessage(InvalidNumberMessage("Nip"))
            .Length(10);
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().Must(x => int.TryParse(x, out var result)).WithMessage(InvalidNumberMessage("Phone"))
            .Length(9);

    }
}