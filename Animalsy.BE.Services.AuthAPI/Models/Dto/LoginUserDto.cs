﻿namespace Animalsy.BE.Services.AuthAPI.Models.Dto;

public record LoginUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}