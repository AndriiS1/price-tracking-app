using System.Security.Claims;
using Domain;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerPresentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlController(IUnitOfWork unitOfWork, IUrlShortenerService urlShortenerService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetTableUrlsController()
    {
        var userIdFromToken = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdFromToken == null)
        {
            return Forbid();
        }

        var userId = Guid.Parse(userIdFromToken);
        var foundUser = unitOfWork.Users.FirstOrDefault(e => e.Id == Guid.Parse(userIdFromToken));
        if (foundUser?.Role == UserRole.Admin)
        {
            return Ok(unitOfWork.Urls.GetAllAdminTableUrls().ToList());
        }
        else
        {
            return Ok();
        }
    }

    [HttpPost]
    public IActionResult CreateUrlController(ShortenUrlDto shortenUrlDto)
    {
        var userIdFromToken = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdFromToken != null)
        {
            var userId = long.Parse(userIdFromToken);
            var tryOriginalUrl =
                unitOfWork.Urls.SingleOrDefault(u => string.Compare(u.OriginalUrl, shortenUrlDto.OriginalUrl) == 0);
            if (tryOriginalUrl != null)
            {
                return BadRequest("This url is already shorten");
            }

            var generatedShortUrl = "";
            var code = "";
            while (true)
            {
                var saltedPrefix = "";
                var serverAddress = string.Format("{0}://{1}",
                    HttpContext.Request.Scheme, HttpContext.Request.Host);
                code = urlShortenerService.GenerageShortUrl(shortenUrlDto?.OriginalUrl?.ToLower() + saltedPrefix);
                generatedShortUrl = serverAddress + "/" + code;
                var tryFindShortUrl = unitOfWork.Urls.SingleOrDefault(u => u.ShortUrl == generatedShortUrl);
                if (tryFindShortUrl == null)
                {
                    break;
                }
                else
                {
                    var random = new Random();
                }
            }

            var urlInstance = new Url
            {
                Date = DateTime.Now,
                OriginalUrl = shortenUrlDto?.OriginalUrl,
                UserId = userId,
                Code = code,
                ShortUrl = generatedShortUrl
            };
            unitOfWork.Urls.Add(urlInstance);
            unitOfWork.Complete();
            return Ok();
        }

        return Unauthorized();
    }

    [HttpGet("{id:long}")]
    public IActionResult GetUrlInfo(long id)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;

        if (claimsIdentity != null)
        {
            return Unauthorized();
        }

        var foundUrl = unitOfWork.Urls.GetUrlWithLoadedUserData(id);
        if (foundUrl != null)
        {
            return Ok(new UrlInfoDto
            {
                Id = foundUrl.Id,
                OriginalUrl = foundUrl.OriginalUrl,
                ShortUrl = foundUrl.ShortUrl,
                Date = foundUrl.Date,
                UserName = $"{foundUrl.User?.FirstName} {foundUrl.User?.SecondName}"
            });
        }
        else
        {
            return BadRequest("Url with this id is not found.");
        }
    }

    [HttpDelete("{id:long}")]
    public IActionResult DeleteUrl(long id)
    {
        var userIdFromToken = (User.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdFromToken == null)
        {
            return Unauthorized();
        }


        return BadRequest("Url with this id is not found.");
    }
}