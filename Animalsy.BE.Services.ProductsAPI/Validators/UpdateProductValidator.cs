using Animalsy.BE.Services.ProductAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.ProductAPI.Validators;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Category).NotEmpty().MaximumLength(20);
        RuleFor(x => x.SubCategory).NotEmpty().MaximumLength(20);
        RuleFor(x => x.MinPrice).NotEmpty();
        RuleFor(x => x.Duration).NotEmpty();
    }
}