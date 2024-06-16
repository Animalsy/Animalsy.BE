using Animalsy.BE.Services.AuthAPI.Models.Dto;
using FluentValidation;

namespace Animalsy.BE.Services.AuthAPI.Validators;

public class LoginUserValidator : AbstractValidator<LoginUserDto>
{
    internal static string InvalidEmailAddressMessage = "Email address is not in correct format";
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(InvalidEmailAddressMessage).MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
    }
}