namespace Animalsy.BE.Services.AuthAPI.Configuration;

public record JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
}