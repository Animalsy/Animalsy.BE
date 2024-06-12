using Animalsy.BE.Services.AuthAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.AuthAPI.Validators;

public class LoginUserValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserValidator(EmailValidator emailValidator)
    {
        RuleFor(x => x.Email).SetValidator(emailValidator);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
    }
}