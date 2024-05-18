using Animalsy.BE.Services.PetsAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.PetsAPI.Validators;

public class CreatePetValidator : AbstractValidator<CreatePetDto>
{
    public CreatePetValidator(UniqueIdValidator idValidator)
    {
        RuleFor(x => x.CustomerId).SetValidator(idValidator);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Species).NotEmpty().MaximumLength(40);
        RuleFor(x => x.Race).NotEmpty().MaximumLength(20);
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.ImageUrl).MaximumLength(500).When(x => x is not null);
    }
}