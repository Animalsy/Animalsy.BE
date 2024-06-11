using Animalsy.BE.Services.AuthAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.AuthAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<UniqueIdValidator>()
            .AddScoped<EmailValidator>()
            .AddScoped<PhoneNumberValidator>()
            .AddScoped<CreateCustomerValidator>()
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}