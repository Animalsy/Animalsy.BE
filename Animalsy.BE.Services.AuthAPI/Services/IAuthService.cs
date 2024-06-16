using Animalsy.BE.Services.AuthAPI.Models.Dto;

namespace Animalsy.BE.Services.AuthAPI.Services;

public interface IAuthService
{
    Task<ResponseDto> RegisterAsync(RegisterUserDto registerUserDto);
    Task<ResponseDto> LoginAsync(LoginUserDto loginUserDto);
    Task<ResponseDto> AssignRoleAsync(AssignRoleDto assignRoleDto);
}