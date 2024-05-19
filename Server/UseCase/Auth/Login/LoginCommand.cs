using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UseCase.Auth.Login;

public record LoginCommand(string Email, string Password) : IRequest<IActionResult>;