using Animalsy.BE.Services.VendorAPI.Models.Dto;

namespace Animalsy.BE.Services.VendorAPI.Services;

public interface IAuthService
{
    Task<ResponseDto> AssignRoleAsync(AssignRoleDto assignRoleDto);
}