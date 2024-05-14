using FluentValidation;

namespace Animalsy.BE.Services.CustomersAPI.Validators
{
    public class UniqueIdValidator : AbstractValidator<Guid>
    {
        internal static string InvalidUniqueIdMessage = "Customer Id is not in valid format";
        public UniqueIdValidator()
        {
            RuleFor(x => x).Must(x => x != Guid.Empty).WithMessage(InvalidUniqueIdMessage);
        }
    }
}
