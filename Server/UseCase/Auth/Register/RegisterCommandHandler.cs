using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace UseCase.Auth.Register;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    IHashService hashService,
    IJwtService jwtService,
    IValidationService validationService)
    : IRequestHandler<RegisterCommand, IActionResult>
{
    public async Task<IActionResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var tryFindExistingUser = await userRepository.GetByEmail(command.Email);
        if (tryFindExistingUser != null)
        {
            return new BadRequestObjectResult("User with this email already exists.");
        }

        var user = new User()
        {
            Id = ObjectId.GenerateNewId(),
            FirstName = command.FirstName,
            SecondName = command.SecondName,
            Email = command.Email,
            Password = hashService.GetHash(command.Password ?? ""),
            Role = UserRole.Basic
        };

        await userRepository.Create(user);
        var accessToken = jwtService.GenerateJsonWebToken(user);
        var (refreshToken, refreshTokenExpiryTime) = jwtService.GenerateRefreshTokenData();
        await userRepository.UpdateUserRefreshTokenData(user.Id, refreshToken, refreshTokenExpiryTime);

        if (!validationService.UserIsValid(user))
        {
            return new BadRequestObjectResult("User data is not valid.");
        }

        return new OkObjectResult(new { accessToken, refreshToken });
    }
}