using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCase.Products.Get_all;
using UseCase.Products.Search;

namespace ServerPresentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(
    ISender mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> SearchController(SearchProductQuery query)
    {
        return await mediator.Send(query);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("tracked-products")]
    public async Task<IActionResult> TrackedProductsController(int page, int size)
    {
        return await mediator.Send(new GetTrackedProductsQuery() { Page = page, Size = size });
    }
}