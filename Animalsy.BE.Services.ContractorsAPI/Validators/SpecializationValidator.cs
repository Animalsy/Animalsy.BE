using Animalsy.BE.Services.ContractorAPI.Models.Enum;
using FluentValidation;

namespace Animalsy.BE.Services.ContractorAPI.Validators;

public class CategoryValidator : AbstractValidator<string>
{
    public CategoryValidator(bool isActionCreate)
    {
        RuleFor(x => x)
            .NotEmpty()
            .Must(x => ValidateCategoryAndSubCategory(x, isActionCreate))
            .WithMessage("Invalid specialization")
            .MaximumLength(60);
    }

    private static bool ValidateCategoryAndSubCategory(string categoryAndSubcategory, bool isActionCreate)
    {
        return isActionCreate
            ? BeValidCategoryAndSubcategory(categoryAndSubcategory)
            : BeValidCategoryOrSubcategory(categoryAndSubcategory);
    }

    private static bool BeValidCategoryAndSubcategory(string categoryAndSubcategory)
    {
        var parts = categoryAndSubcategory.Split('.');
        if (parts.Length != 2) return false;


        return IsValidCategory(parts[0], out var category) && IsValidSubcategory(category, parts[1]);
    }

    private static bool BeValidCategoryOrSubcategory(string categoryAndSubcategory)
    {
        var parts = categoryAndSubcategory.Split('.');

        return parts.Length switch
        {
            1 => IsValidCategory(parts[0], out _),
            2 => IsValidCategory(parts[0], out var category) && IsValidSubcategory(category, parts[1]),
            _ => false
        };
    }

    private static bool IsValidSubcategory(Category category, string subcategory)
    {
        return category switch
        {
            Category.PetGrooming => Enum.TryParse<PetGrooming>(subcategory, true, out _),
            Category.VeterinaryCare => Enum.TryParse<VeterinaryCare>(subcategory, true, out _),
            Category.PetSupplies => Enum.TryParse<PetSupplies>(subcategory, true, out _),
            Category.PetBoarding => Enum.TryParse<PetBoarding>(subcategory, true, out _),
            Category.PetTraining => Enum.TryParse<PetTraining>(subcategory, true, out _),
            Category.PetSitting => Enum.TryParse<PetSitting>(subcategory, true, out _),
            Category.PetTransport => Enum.TryParse<PetTransport>(subcategory, true, out _),
            Category.PetHealthInsurance => Enum.TryParse<PetHealthInsurance>(subcategory, true, out _),
            Category.PetPhotography => Enum.TryParse<PetPhotography>(subcategory, true, out _),
            Category.PetAdoption => Enum.TryParse<PetAdoption>(subcategory, true, out _),
            _ => false
        };
    }

    private static bool IsValidCategory(string value, out Category category)
    {
        return Enum.TryParse(value, true, out category);
    }
}