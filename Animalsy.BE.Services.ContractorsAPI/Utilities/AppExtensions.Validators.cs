using Animalsy.BE.Services.ContractorsAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.ContractorsAPI.Utilities
{
    public static partial class AppExtensions
    {
        public static void AddValidators(this IServiceCollection serviceCollection)
        {
            DisableValidationTranslation();

            serviceCollection
                .AddScoped<UniqueIdValidator>()
                .AddScoped<CreateContractorValidator>()
                .AddScoped<UpdateContractorValidator>();
        }

        private static void DisableValidationTranslation()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;
        }
    }
}
