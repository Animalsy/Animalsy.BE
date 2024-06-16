using Animalsy.BE.Services.AuthAPI.Models.Dto;
using Animalsy.BE.Services.AuthAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.AuthAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<IValidator<AssignRoleDto>, AssignRoleValidator>()
            .AddScoped<IValidator<LoginUserDto>, LoginUserValidator>()
            .AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}