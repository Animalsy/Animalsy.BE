using FluentValidation;

namespace Animalsy.BE.Services.VisitAPI.Validators.Factory;

public interface IValidatorFactory
{
    IValidator<T> GetValidator<T>();
}