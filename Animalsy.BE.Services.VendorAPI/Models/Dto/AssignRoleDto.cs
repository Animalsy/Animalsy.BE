namespace Animalsy.BE.Services.VendorAPI.Models.Dto;

public record AssignRoleDto
{
    public string Email { get; init; }
    public string RoleName { get; init; }
}