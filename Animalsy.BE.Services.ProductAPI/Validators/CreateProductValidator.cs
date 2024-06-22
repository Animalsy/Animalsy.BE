using Animalsy.BE.Services.ProductAPI.Models.Dto;
using FluentValidation;
using IValidatorFactory = Animalsy.BE.Services.ProductAPI.Validators.Factory.IValidatorFactory;

namespace Animalsy.BE.Services.ProductAPI.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator(IValidatorFactory validatorFactory)
    {
        var factory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));

        RuleFor(x => x.VendorId).SetValidator(factory.GetValidator<Guid>());
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.MinPrice).NotEmpty();
        RuleFor(x => x.Duration).NotEmpty();
        RuleFor(x => x.CategoryAndSubCategory).SetValidator(new CategoryValidator(true));
    }
}