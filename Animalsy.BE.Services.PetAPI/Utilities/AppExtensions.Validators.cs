using Animalsy.BE.Services.PetAPI.Models.Dto;
using Animalsy.BE.Services.PetAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.PetAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<IValidator<Guid>, UniqueIdValidator>()
            .AddScoped<IValidator<CreatePetDto>, CreatePetValidator>()
            .AddScoped<IValidator<UpdatePetDto>, UpdatePetValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}