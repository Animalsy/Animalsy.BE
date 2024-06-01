using Animalsy.BE.Services.VisitAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.VisitAPI.Validators
{
    public class UpdateVisitValidator : AbstractValidator<UpdateVisitDto>
    {
        public UpdateVisitValidator(UniqueIdValidator idValidator)
        {
            RuleFor(x => x.Id).SetValidator(idValidator);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Comment).NotEmpty().When(x => x.Comment is not null);
            RuleFor(x => x.State).NotEmpty().When(x => x.State is not null);
        }
    }
}
