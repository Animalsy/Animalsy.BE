using FluentValidation;

namespace Animalsy.BE.Services.AuthAPI.Validators;

public class UniqueIdValidator : AbstractValidator<Guid>
{
    internal static string InvalidUniqueIdMessage = "Id should be non empty Guid";
    public UniqueIdValidator()
    {
        RuleFor(x => x).Must(x => x != Guid.Empty).WithMessage(InvalidUniqueIdMessage);
    }
}