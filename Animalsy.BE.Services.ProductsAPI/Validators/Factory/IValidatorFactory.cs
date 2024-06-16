using FluentValidation;

namespace Animalsy.BE.Services.ProductAPI.Validators.Factory;

public interface IValidatorFactory
{
    IValidator<T> GetValidator<T>();
}