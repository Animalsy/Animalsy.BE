using Animalsy.BE.Services.ProductsAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.ProductsAPI.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator(UniqueIdValidator idValidator)
    {
        RuleFor(x => x.VendorId).SetValidator(idValidator);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Category).NotEmpty().MaximumLength(20);
        RuleFor(x => x.SubCategory).NotEmpty().MaximumLength(20);
        RuleFor(x => x.MinPrice).NotEmpty();
        RuleFor(x => x.Duration).NotEmpty();
    }
}