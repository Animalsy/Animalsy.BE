using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.VendorAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<IValidator<Guid>, UniqueIdValidator>()
            .AddScoped<IValidator<CreateVendorDto>, CreateVendorValidator>()
            .AddScoped<IValidator<UpdateVendorDto>, UpdateVendorValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}