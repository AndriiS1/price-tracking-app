using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UseCase.Auth.Register;

public record RegisterCommand : IRequest<IActionResult>
{
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}