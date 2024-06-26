﻿using Microsoft.AspNetCore.Identity;

namespace Animalsy.BE.Services.AuthAPI.Models;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() : base() { }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}