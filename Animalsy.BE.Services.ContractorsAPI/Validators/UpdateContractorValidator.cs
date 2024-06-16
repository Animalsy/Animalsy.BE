using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using FluentValidation;
using IValidatorFactory = Animalsy.BE.Services.ContractorAPI.Validators.Factory.IValidatorFactory;

namespace Animalsy.BE.Services.ContractorAPI.Validators;

public class UpdateContractorValidator : AbstractValidator<UpdateContractorDto>
{
    public UpdateContractorValidator(IValidatorFactory validatorFactory)
    {
        var factory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));

        RuleFor(x => x.Id).SetValidator(factory.GetValidator<Guid>());
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Specialization).NotEmpty().MaximumLength(400);
        RuleFor(x => x.ImageUrl).MaximumLength(500).When(x => x is not null);
    }
}