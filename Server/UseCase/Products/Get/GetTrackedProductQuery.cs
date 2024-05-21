using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UseCase.Products.Get;

public record GetTrackedProductQuery : IRequest<IActionResult>
{
    public required string Id { get; set; }
}