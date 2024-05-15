using Animalsy.BE.Services.PetsAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.PetsAPI.Utilities;

internal static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<UniqueIdValidator>()
            .AddScoped<CreatePetValidator>()
            .AddScoped<UpdatePetValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}