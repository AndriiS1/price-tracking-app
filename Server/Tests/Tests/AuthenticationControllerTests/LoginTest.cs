using System.Linq.Expressions;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerPresentation.Controllers;

namespace Tests.Tests.AuthenticationControllerTests;

public class LoginTest
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        private AuthenticationController _authenticationController;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IJwtService> _jwtServiceMock;
        private Mock<IHashService> _hashServiceMock;
        private Mock<IValidationService> _validationServiceMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _jwtServiceMock = new Mock<IJwtService>();
            _hashServiceMock = new Mock<IHashService>();
            _validationServiceMock = new Mock<IValidationService>();

            _authenticationController = new AuthenticationController(
                _unitOfWorkMock.Object,
                _jwtServiceMock.Object,
                _hashServiceMock.Object,
                _validationServiceMock.Object
            );
        }

        [Test]
        public void LoginController_ValidLoginData_ReturnsOkResultWithAccessTokenAndRefreshToken()
        {
            const string userPassword = "1ecxxcJsdEREw";
            // Arrange
            var loginData = new LoginDto
            {
                Email = "test@example.com",
                Password = userPassword
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Password = "hashedPassword",
                FirstName = "text",
                SecondName = "test",
                Role = UserRole.Basic
            };

            const string accessToken = "accessToken";
            var refreshTokenDataDto = new RefreshTokenDataDto
            {
                RefreshToken = "refreshToken"
            };

            _unitOfWorkMock.Setup(u => u.Users.SingleOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            _jwtServiceMock.Setup(j => j.GenerateJsonWebToken(user)).Returns(accessToken);
            _jwtServiceMock.Setup(j => j.GenerateRefreshTokenData()).Returns(refreshTokenDataDto);
            _hashServiceMock.Setup(h => h.GetHash(userPassword)).Returns("hashedPassword");

            // Act
            var result = _authenticationController.LoginController(loginData) as OkObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.StatusCode, Is.EqualTo(200));
                Assert.IsNotNull(result?.Value);
                Assert.That(result?.Value?.GetType()?.GetProperty("accessToken")?.GetValue(result.Value),
                    Is.EqualTo(accessToken));
                Assert.That(result?.Value?.GetType()?.GetProperty("refreshToken")?.GetValue(result.Value),
                    Is.EqualTo(refreshTokenDataDto.RefreshToken));
            });
        }

        [Test]
        public void LoginController_InvalidLoginData_ReturnsUnauthorizedResult()
        {
            // Arrange
            var loginData = new LoginDto
            {
                Email = "test@example.com",
                Password = "password"
            };

            _unitOfWorkMock.Setup(u => u.Users.SingleOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns((User?)null);

            // Act
            var result = _authenticationController.LoginController(loginData) as UnauthorizedObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.StatusCode, Is.EqualTo(401));
                Assert.That(result?.Value, Is.EqualTo("User not found."));
            });
        }
    }
}