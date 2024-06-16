using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Animalsy.BE.Gateway.Utilities;

public static class AppExtensions
{
    public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
    {
        var jwtOptionsSection = builder.Configuration.GetSection("JwtOptions");

        var secret = jwtOptionsSection.GetValue<string>("Secret");
        var issuer = jwtOptionsSection.GetValue<string>("Issuer");
        var audience = jwtOptionsSection.GetValue<string>("Audience");

        var key = Encoding.ASCII.GetBytes(secret!);

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ValidateAudience = true
            };
        });

        return builder;
    }
}