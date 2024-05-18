using Animalsy.BE.Services.CustomersAPI.Validators;
using FluentValidation;
namespace Animalsy.BE.Services.CustomersAPI.Utilities;

internal static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<UniqueIdValidator>()
            .AddScoped<EmailValidator>()
            .AddScoped<PhoneNumberValidator>()
            .AddScoped<CreateCustomerValidator>()
            .AddScoped<UpdateCustomerValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}