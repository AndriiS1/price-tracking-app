using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Models;
using Domain.Services;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtService(IOptions<JwtOptions> jwOptions) : IJwtService
{
    public string GenerateJsonWebToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwOptions.Value.Key));
        _ = int.TryParse(jwOptions.Value.TokenValidityInMinutes, out var tokenValidityInMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.SecondName),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            jwOptions.Value.Issuer,
            jwOptions.Value.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public (string refreshToken, DateTime refreshTokenExpiryTime) GenerateRefreshTokenData()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        _ = int.TryParse(jwOptions.Value.RefreshTokenValidityInDays, out var tokenValidityInMinutes);
        return (Convert.ToBase64String(randomNumber), DateTime.Now.AddMinutes(tokenValidityInMinutes));
    }

    public IEnumerable<Claim>? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwOptions.Value.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        return jsonToken?.Claims;
    }
}