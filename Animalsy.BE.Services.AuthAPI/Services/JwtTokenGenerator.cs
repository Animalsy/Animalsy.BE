using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Animalsy.BE.Services.AuthAPI.Configuration;
using Animalsy.BE.Services.AuthAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Animalsy.BE.Services.AuthAPI.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IOptionsSnapshot<JwtOptions> _jwtOptions;

    public JwtTokenGenerator(IOptionsSnapshot<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string GenerateToken(ApplicationUser applicationUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.Secret);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, applicationUser.Email!),
            new(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Audience = _jwtOptions.Value.Audience,
            Issuer = _jwtOptions.Value.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(10),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(token);
    }
}