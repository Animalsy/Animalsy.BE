using Animalsy.BE.Services.ProductAPI.Models.Dto;
using Animalsy.BE.Services.ProductAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.ProductAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<IValidator<Guid>, UniqueIdValidator>()
            .AddScoped<IValidator<CreateProductDto>, CreateProductValidator>()
            .AddScoped<IValidator<UpdateProductDto>, UpdateProductValidator>()
            .AddScoped<IValidator<VendorCategoryDto>, VendorCategoryValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}