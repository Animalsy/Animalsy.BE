using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using Animalsy.BE.Services.ContractorAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.ContractorAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<IValidator<Guid>, UniqueIdValidator>()
            .AddScoped<IValidator<CreateContractorDto>, CreateContractorValidator>()
            .AddScoped<IValidator<UpdateContractorDto>, UpdateContractorValidator>()
            .AddScoped<IValidator<ContractorSpecializationDto>, ContractorSpecializationValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}