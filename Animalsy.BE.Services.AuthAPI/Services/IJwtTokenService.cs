﻿using Animalsy.BE.Services.AuthAPI.Models;

namespace Animalsy.BE.Services.AuthAPI.Services;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser applicationUser, IList<string> userRoles);
}