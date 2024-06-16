using FluentValidation;

namespace Animalsy.BE.Services.AuthAPI.Validators.Factory;

public interface IValidatorFactory
{
    IValidator<T> GetValidator<T>();
}