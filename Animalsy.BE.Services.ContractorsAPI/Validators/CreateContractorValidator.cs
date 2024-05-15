using Animalsy.BE.Services.ContractorsAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.ContractorsAPI.Validators;

public class CreateContractorValidator : AbstractValidator<CreateContractorDto>
{
    public CreateContractorValidator(UniqueIdValidator idValidator)
    {
        RuleFor(x => x.VendorId).SetValidator(idValidator);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Specialization).NotEmpty().MaximumLength(400);
        RuleFor(x => x.ImageUrl).MaximumLength(500).When(x => x is not null);
    }
}