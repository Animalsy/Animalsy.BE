using Animalsy.BE.Services.VendorAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.VendorAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<UniqueIdValidator>()
            .AddScoped<EmailValidator>()
            .AddScoped<CreateVendorValidator>()
            .AddScoped<UpdateVendorValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}