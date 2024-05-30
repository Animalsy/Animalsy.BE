using FluentValidation;

namespace Animalsy.BE.Services.ContractorAPI.Validators;

public class UniqueIdValidator : AbstractValidator<Guid>
{
    internal static string InvalidUniqueIdMessage = "Contractor Id is not in valid format";
    public UniqueIdValidator()
    {
        RuleFor(x => x).Must(x => x != Guid.Empty).WithMessage(InvalidUniqueIdMessage);
    }
}