using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCase.Auth.Login;
using UseCase.Auth.RefreshToken;
using UseCase.Auth.Register;

namespace ServerPresentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(
    ISender mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginController(LoginCommand command)
    {
        return await mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
    {
        return await mediator.Send(command);
    }
}