using System.IdentityModel.Tokens.Jwt;
using Domain.Repositories;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace UseCase.Auth.RefreshToken;

public class RefreshTokenCommandHandler(IUserRepository userRepository, IJwtService jwtService)
    : IRequestHandler<RefreshTokenCommand, IActionResult>
{
    public async Task<IActionResult> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var claims = jwtService.GetPrincipalFromExpiredToken(command.AccessToken);
        if (claims == null)
        {
            return new BadRequestObjectResult("Invalid access token or refresh token");
        }

        var userId = claims.Single(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value;

        var user = await userRepository.GetById(ObjectId.Parse(userId));

        if (user == null || user.RefreshToken != command.AccessToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return new BadRequestObjectResult("Invalid access token or refresh token");
        }

        var accessToken = jwtService.GenerateJsonWebToken(user);
        var (refreshToken, refreshTokenExpiryTime) = jwtService.GenerateRefreshTokenData();
        await userRepository.UpdateUserRefreshTokenData(user.Id, refreshToken,
            refreshTokenExpiryTime);

        return new OkObjectResult(new
        {
            accessToken,
            refreshToken
        });
    }
}