using FluentValidation;

namespace Animalsy.BE.Services.ContractorAPI.Validators.Factory;

public class ValidatorFactory : IValidatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IValidator<T> GetValidator<T>()
    {
        return _serviceProvider.GetRequiredService<IValidator<T>>();
    }
}