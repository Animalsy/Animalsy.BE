using Animalsy.BE.Services.ProductAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.ProductAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<UniqueIdValidator>()
            .AddScoped<CreateProductValidator>()
            .AddScoped<UpdateProductValidator>()
            .AddScoped<VendorCategoryValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}