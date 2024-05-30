using FluentValidation;

namespace Animalsy.BE.Services.VendorAPI.Validators;

public class UniqueIdValidator : AbstractValidator<Guid>
{
    internal static string InvalidUniqueIdMessage = "Vendor Id is not in valid format";
    public UniqueIdValidator()
    {
        RuleFor(x => x).Must(x => x != Guid.Empty).WithMessage(InvalidUniqueIdMessage);
    }
}