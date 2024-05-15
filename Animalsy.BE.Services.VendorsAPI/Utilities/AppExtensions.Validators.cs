using Animalsy.BE.Services.VendorsAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.VendorsAPI.Utilities
{
    internal static partial class AppExtensions
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
}
