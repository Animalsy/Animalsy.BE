﻿using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using FluentValidation;
using IValidatorFactory = Animalsy.BE.Services.ContractorAPI.Validators.Factory.IValidatorFactory;

namespace Animalsy.BE.Services.ContractorAPI.Validators;

public class CreateContractorValidator : AbstractValidator<CreateContractorDto>
{
    public CreateContractorValidator(IValidatorFactory validatorFactory)
    {
        var factory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));

        RuleFor(x => x.VendorId).SetValidator(factory.GetValidator<Guid>());
        RuleFor(x => x.UserId).SetValidator(factory.GetValidator<Guid>());
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.ImageUrl).MaximumLength(500).When(x => x is not null);
        RuleFor(x => x.Specialization).SetValidator(new CategoryValidator(true));
    }
}