using Animalsy.BE.Services.VisitAPI.Models.Dto;
using FluentValidation;
using IValidatorFactory = Animalsy.BE.Services.VisitAPI.Validators.Factory.IValidatorFactory;

namespace Animalsy.BE.Services.VisitAPI.Validators
{
    public class UpdateVisitValidator : AbstractValidator<UpdateVisitDto>
    {
        public UpdateVisitValidator(IValidatorFactory validatorFactory)
        {
            var factory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));

            RuleFor(x => x.Id).SetValidator(factory.GetValidator<Guid>());
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Comment).NotEmpty().When(x => x.Comment is not null);
            RuleFor(x => x.State).NotEmpty().When(x => x.State is not null);
        }
    }
}
