using Animalsy.BE.Services.AuthAPI.Models.Dto;
using Animalsy.BE.Services.AuthAPI.Models.Enums;
using FluentValidation;

namespace Animalsy.BE.Services.AuthAPI.Validators;

public class AssignRoleValidator : AbstractValidator<AssignRoleDto>
{
    public AssignRoleValidator(EmailValidator emailValidator)
    {
        RuleFor(x => x.Email).SetValidator(emailValidator);
        RuleFor(x => x.RoleName).NotEmpty().Must(x => Enum.TryParse<UserRole>(x, true, out var roleName));
    }
}