using Animalsy.BE.Services.PetAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.PetAPI.Validators;

public class UpdatePetValidator : AbstractValidator<UpdatePetDto>
{
    public UpdatePetValidator(UniqueIdValidator idValidator)
    {
        RuleFor(x => x.Id).SetValidator(idValidator);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Species).NotEmpty().MaximumLength(40);
        RuleFor(x => x.Race).NotEmpty().MaximumLength(20);
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.ImageUrl).MaximumLength(500).When(x => x is not null);
    }
}