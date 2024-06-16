using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Animalsy.BE.Services.AuthAPI.Configuration;
using Animalsy.BE.Services.AuthAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Animalsy.BE.Services.AuthAPI.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IOptionsSnapshot<JwtOptions> _jwtOptions;

    public JwtTokenService(IOptionsSnapshot<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string GenerateToken(ApplicationUser applicationUser, IList<string> userRoles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.Secret);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, applicationUser.Email!),
            new(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString())
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var descriptor = new SecurityTokenDescriptor
        {
            Audience = _jwtOptions.Value.Audience,
            Issuer = _jwtOptions.Value.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.Add(_jwtOptions.Value.ExpirationTime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(token);
    }
}