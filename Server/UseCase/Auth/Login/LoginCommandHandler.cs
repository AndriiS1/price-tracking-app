using Domain.Repositories;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UseCase.Auth.Login;

public class LoginCommandHandler(IUserRepository userRepository, IHashService hashService, IJwtService jwtService)
    : IRequestHandler<LoginCommand, IActionResult>
{
    public async Task<IActionResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.Get(hashService.GetHash(command.Password), command.Email);

        if (user == null)
        {
            return new UnauthorizedObjectResult("User not found.");
        }

        var accessToken = jwtService.GenerateJsonWebToken(user);
        var refreshTokenData = jwtService.GenerateRefreshTokenData();
        await userRepository.UpdateUserRefreshTokenData(user.Id, refreshTokenData.refreshToken,
            refreshTokenData.refreshTokenExpiryTime);

        return new OkObjectResult(new { accessToken, refreshToken = refreshTokenData.refreshToken });
    }
}