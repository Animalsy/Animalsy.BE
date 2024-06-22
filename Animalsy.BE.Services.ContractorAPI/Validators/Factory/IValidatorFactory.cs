using FluentValidation;

namespace Animalsy.BE.Services.ContractorAPI.Validators.Factory;

public interface IValidatorFactory
{
    IValidator<T> GetValidator<T>();
}