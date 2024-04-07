using System.IdentityModel.Tokens.Jwt;
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
public class AuthenticationController(
    IUnitOfWork unitOfWork,
    IJwtService jwtService,
    IHashService hashService,
    IValidationService validationService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public IActionResult LoginController(LoginDto loginData)
    {
        IActionResult response = Unauthorized("User not found.");
        var user = unitOfWork.Users.SingleOrDefault(u =>
            u.Password == hashService.GetHash(loginData.Password) && u.Email == loginData.Email);
        if (user != null)
        {
            var accessToken = jwtService.GenerateJSONWebToken(user);
            var refreshTokenDataDto = jwtService.GenerateRefreshTokenData();
            unitOfWork.Users.UpdateUserRefreshTokenData(user.Id, refreshTokenDataDto);
            unitOfWork.Complete();
            response = Ok(new { accessToken, refreshToken = refreshTokenDataDto.RefreshToken });
        }

        return response;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public IActionResult Register(User user)
    {
        if (validationService.UserIsValid(user))
        {
            var tryFindExistingUser = unitOfWork.Users.FirstOrDefault(u => u.Email == user.Email);
            if (tryFindExistingUser != null)
            {
                return BadRequest("User with this email already exists.");
            }

            user.Password = hashService.GetHash(user.Password ?? "");
            user.Role = UserRole.Basic;
            unitOfWork.Users.Add(user);
            unitOfWork.Complete();
            var accessToken = jwtService.GenerateJSONWebToken(user);
            var refreshTokenDataDto = jwtService.GenerateRefreshTokenData();
            unitOfWork.Users.UpdateUserRefreshTokenData(user.Id, refreshTokenDataDto);
            unitOfWork.Complete();
            return Ok(new { accessToken, refreshToken = refreshTokenDataDto.RefreshToken });
        }
        else
        {
            return BadRequest("User data is not valid.");
        }
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenDto tokenData)
    {
        var accessToken = tokenData.AccessToken;
        var refreshToken = tokenData.RefreshToken;

        var claims = jwtService.GetPrincipalFromExpiredToken(accessToken);
        if (claims == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var userId = claims.Single(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value;

        var user = unitOfWork.Users.Single(u => u.Id == Guid.Parse(userId));

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = jwtService.GenerateJSONWebToken(user);
        var newRefreshTokenDataDto = jwtService.GenerateRefreshTokenData();
        unitOfWork.Users.UpdateUserRefreshTokenData(user.Id, newRefreshTokenDataDto);
        unitOfWork.Complete();

        return Ok(new
        {
            accessToken = newAccessToken,
            refreshToken = newRefreshTokenDataDto.RefreshToken
        });
    }
}