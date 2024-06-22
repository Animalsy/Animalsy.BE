using Animalsy.BE.Services.ProductAPI.Models.Dto;
using FluentValidation;
using IValidatorFactory = Animalsy.BE.Services.ProductAPI.Validators.Factory.IValidatorFactory;

namespace Animalsy.BE.Services.ProductAPI.Validators;

public class VendorCategoryValidator : AbstractValidator<VendorCategoryDto>
{
    public VendorCategoryValidator(IValidatorFactory validatorFactory)
    {
        var factory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));

        RuleFor(x => x.VendorId).SetValidator(factory.GetValidator<Guid>());
        RuleFor(x => x.CategoryAndSubCategory).SetValidator(new CategoryValidator(false));
    }
}