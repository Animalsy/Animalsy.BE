using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using FluentValidation;
using IValidatorFactory = Animalsy.BE.Services.ContractorAPI.Validators.Factory.IValidatorFactory;

namespace Animalsy.BE.Services.ContractorAPI.Validators;

public class ContractorSpecializationValidator : AbstractValidator<ContractorSpecializationDto>
{
    public ContractorSpecializationValidator(IValidatorFactory validatorFactory)
    {
        var factory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));

        RuleFor(x => x.ContractorId).SetValidator(factory.GetValidator<Guid>());
        RuleFor(x => x.Specialization).SetValidator(new CategoryValidator(false));
    }
}