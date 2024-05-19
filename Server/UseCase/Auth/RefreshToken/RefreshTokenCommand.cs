using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UseCase.Auth.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<IActionResult>;