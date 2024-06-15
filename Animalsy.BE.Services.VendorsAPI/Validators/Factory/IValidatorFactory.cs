using FluentValidation;

namespace Animalsy.BE.Services.VendorAPI.Validators.Factory;

public interface IValidatorFactory
{
    IValidator<T> GetValidator<T>();
}