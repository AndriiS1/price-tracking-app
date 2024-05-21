using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UseCase.Products.Get_all;

public record GetTrackedProductsQuery : IRequest<IActionResult>
{
    public required int Page { get; set; }
    public required int Size { get; set; }
}