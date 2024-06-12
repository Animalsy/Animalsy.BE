using Animalsy.BE.Services.AuthAPI.Models;

namespace Animalsy.BE.Services.AuthAPI.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser);
}