using FluentValidation;

namespace Animalsy.BE.Services.CustomerAPI.Validators.Factory;

public interface IValidatorFactory
{
    IValidator<T> GetValidator<T>();
}