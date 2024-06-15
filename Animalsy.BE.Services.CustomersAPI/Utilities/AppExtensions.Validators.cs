﻿using Animalsy.BE.Services.CustomerAPI.Validators;
using FluentValidation;

namespace Animalsy.BE.Services.CustomerAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        DisableValidationTranslation();

        serviceCollection
            .AddScoped<UniqueIdValidator>()
            .AddScoped<CreateCustomerValidator>()
            .AddScoped<UpdateCustomerValidator>();
    }

    private static void DisableValidationTranslation()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}