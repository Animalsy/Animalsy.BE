﻿namespace Animalsy.BE.Services.CustomerAPI.Configuration;

public record JwtOptions
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string Secret { get; init; }
    public TimeSpan ExpirationTime { get; init; }
}