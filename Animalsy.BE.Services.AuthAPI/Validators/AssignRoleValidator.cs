using Animalsy.BE.Services.AuthAPI.Models.Dto;
using Animalsy.BE.Services.AuthAPI.Models.Enums;
using FluentValidation;

namespace Animalsy.BE.Services.AuthAPI.Validators;

public class AssignRoleValidator : AbstractValidator<AssignRoleDto>
{
    internal static string InvalidEmailAddressMessage = "Email address is not in correct format";
    internal static string InvalidRoleMessage = "Provided role does not exist";
    public AssignRoleValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage(InvalidEmailAddressMessage).MaximumLength(50);
        RuleFor(x => x.RoleName).NotEmpty().Must(x => Enum.TryParse<UserRole>(x, true, out var roleName)).WithMessage(InvalidRoleMessage);
    }
}