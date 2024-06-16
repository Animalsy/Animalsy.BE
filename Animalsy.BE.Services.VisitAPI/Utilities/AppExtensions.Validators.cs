using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.VisitAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<IValidator<Guid>, UniqueIdValidator>()
            .AddScoped<IValidator<CreateVisitDto>, CreateVisitValidator>()
            .AddScoped<IValidator<UpdateVisitDto>, UpdateVisitValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}