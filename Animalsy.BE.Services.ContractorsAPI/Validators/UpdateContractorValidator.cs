using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.ContractorAPI.Validators;

public class UpdateContractorValidator : AbstractValidator<UpdateContractorDto>
{
    public UpdateContractorValidator(UniqueIdValidator idValidator)
    {
        RuleFor(x => x.Id).SetValidator(idValidator);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Specialization).NotEmpty().MaximumLength(400);
        RuleFor(x => x.ImageUrl).MaximumLength(500).When(x => x is not null);
    }
}