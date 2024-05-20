using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}