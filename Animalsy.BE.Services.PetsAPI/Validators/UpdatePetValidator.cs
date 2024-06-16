using Animalsy.BE.Services.PetAPI.Models.Dto;
using FluentValidation;
using IValidatorFactory = Animalsy.BE.Services.PetAPI.Validators.Factory.IValidatorFactory;

namespace Animalsy.BE.Services.PetAPI.Validators;

public class UpdatePetValidator : AbstractValidator<UpdatePetDto>
{
    public UpdatePetValidator(IValidatorFactory validatorFactory)
    {
        var factory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));

        RuleFor(x => x.Id).SetValidator(factory.GetValidator<Guid>());
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Species).NotEmpty().MaximumLength(40);
        RuleFor(x => x.Race).NotEmpty().MaximumLength(20);
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.ImageUrl).MaximumLength(500).When(x => x is not null);
    }
}