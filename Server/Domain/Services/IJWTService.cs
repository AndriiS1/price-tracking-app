using System.Security.Claims;
using Domain.Models;

namespace Domain.Services;

public interface IJwtService
{
    string GenerateJsonWebToken(User user);
    (string refreshToken, DateTime refreshTokenExpiryTime) GenerateRefreshTokenData();
    IEnumerable<Claim>? GetPrincipalFromExpiredToken(string? token);
}