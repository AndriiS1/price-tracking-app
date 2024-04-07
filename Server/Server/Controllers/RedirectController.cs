using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerPresentation.Controllers;

[ApiController]
[Route("/")]
public class RedirectController(IUnitOfWork unitOfWork) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{code:long}")]
    public IResult GetTableUrlsController(string code)
    {
        var foundUrl = unitOfWork.Urls.SingleOrDefault(u => u.Code == code);
        if (foundUrl != null)
        {
            return Results.Redirect(foundUrl.OriginalUrl);
        }
        else
        {
            return Results.NotFound();
        }
    }
}