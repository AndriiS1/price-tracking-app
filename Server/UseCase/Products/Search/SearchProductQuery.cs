using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UseCase.Products.Search;

public record SearchProductQuery : IRequest<IActionResult>
{
    public required string ProductName { get; set; }
}