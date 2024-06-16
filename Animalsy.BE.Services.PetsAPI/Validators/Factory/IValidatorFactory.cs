using FluentValidation;

namespace Animalsy.BE.Services.PetAPI.Validators.Factory;

public interface IValidatorFactory
{
    IValidator<T> GetValidator<T>();
}