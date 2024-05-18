using Animalsy.BE.Services.ProductsAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.ProductsAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<UniqueIdValidator>()
            .AddScoped<CreateProductValidator>()
            .AddScoped<UpdateProductValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}