using Microsoft.Extensions.Logging;
using Moq;
using TradeMaster.Application.DTOs;
using TradeMaster.Application.Interfaces;
using TradeMaster.Core.Entities;
using TradeMaster.Core.Interfaces;
using TradeMaster.Infrastructure.Services;
using Xunit;

namespace TradeMaster.Tests.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        private readonly Mock<IJwtService> _jwtServiceMock;

        private readonly Mock<ILogger<AuthService>> _loggerMock;

        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            _jwtServiceMock = new Mock<IJwtService>();

            _loggerMock = new Mock<ILogger<AuthService>>();

            _authService = new AuthService(
                _userRepositoryMock.Object,
                _jwtServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact] 
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange

            var request = new LoginRequestDto
            {
                Email = "kunal@test.com",
                Password = "Password123"
            };

            var user = new User
            {
                USerID = 1,
                Email = "kunal@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123")
            };

            var expectedToken = "sample-jwt-token";

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(user);

            _jwtServiceMock
                .Setup(x => x.GenerateToken(user))
                .Returns(expectedToken);

            // Act

            var result = await _authService.LoginAsync(request);

            // Assert

            Assert.NotNull(result);

            Assert.Equal(expectedToken, result);

            _userRepositoryMock.Verify(
                x => x.GetByEmailAsync(request.Email),
                Times.Once);

            _jwtServiceMock.Verify(
                x => x.GenerateToken(user),
                Times.Once);
        }

        [Fact] // User Not Found
        public async Task LoginAsync_ShouldReturnUserNotFound_WhenUserDoesNotExist()
        {
            // Arrange

            var request = new LoginRequestDto
            {
                Email = "unknown@test.com",
                Password = "Password123"
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((User)null);

            // Act

            var result = await _authService.LoginAsync(request);

            // Assert

            Assert.Equal("User Not Found !", result);

            _userRepositoryMock.Verify(
                x => x.GetByEmailAsync(request.Email),
                Times.Once);

            _jwtServiceMock.Verify(
                x => x.GenerateToken(It.IsAny<User>()),
                Times.Never);
        }

        [Fact] // Invalid Password
        public async Task LoginAsync_ShouldReturnInvalidPassword_WhenPasswordIsIncorrect()
        {
            // Arrange

            var request = new LoginRequestDto
            {
                Email = "kunal@test.com",
                Password = "WrongPassword"
            };

            var user = new User
            {
                USerID = 1,
                Email = "kunal@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123")
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(user);

            // Act

            var result = await _authService.LoginAsync(request);

            // Assert

            Assert.Equal("Invalid Password !", result);

            _userRepositoryMock.Verify(
                x => x.GetByEmailAsync(request.Email),
                Times.Once);

            _jwtServiceMock.Verify(
                x => x.GenerateToken(It.IsAny<User>()),
                Times.Never);
        }

        // REGISTRATION TESTS

        [Fact] // REGISTER SUCCESSFULLY TEST
        public async Task RegisterAsync_ShouldReturnSuccessMessage_WhenRegistrationIsSuccessful()
        {
            // Arrange

            var request = new RegisterRequestDto
            {
                FullName = "Kunal Thakare",
                Email = "kunal@test.com",
                Password = "Password123"
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((User)null);

            _userRepositoryMock
                .Setup(x => x.AddUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act

            var result = await _authService.RegisterAsync(request);

            // Assert

            Assert.Equal("User Registered Successfully !", result);

            _userRepositoryMock.Verify(
                x => x.GetByEmailAsync(request.Email),
                Times.Once);

            _userRepositoryMock.Verify(
                x => x.AddUserAsync(It.IsAny<User>()),
                Times.Once);
        }

        [Fact] // EMAIL ALREADY EXISTS TEST
        public async Task RegisterAsync_ShouldReturnEmailAlreadyExists_WhenEmailIsAlreadyRegistered()
        {
            // Arrange

            var request = new RegisterRequestDto
            {
                FullName = "Kunal Thakare",
                Email = "kunal@test.com",
                Password = "Password123"
            };

            var existingUser = new User
            {
                USerID = 1,
                FullName = "Existing User",
                Email = "kunal@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123")
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(existingUser);

            // Act

            var result = await _authService.RegisterAsync(request);

            // Assert

            Assert.Equal("Email already exists !", result);

            _userRepositoryMock.Verify(
                x => x.GetByEmailAsync(request.Email),
                Times.Once);

            _userRepositoryMock.Verify(
                x => x.AddUserAsync(It.IsAny<User>()),
                Times.Never);
        }

        [Fact] // HVAE HASHPASSWORD BEFORE SAVING TEST
        public async Task RegisterAsync_ShouldHashPasswordBeforeSavingUser()
        {
            // Arrange

            var request = new RegisterRequestDto
            {
                FullName = "Kunal Thakare",
                Email = "kunal@test.com",
                Password = "Password123"
            };

            User savedUser = null;

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((User)null);

            _userRepositoryMock
                .Setup(x => x.AddUserAsync(It.IsAny<User>()))
                .Callback<User>(user => savedUser = user)
                .Returns(Task.CompletedTask);

            // Act

            await _authService.RegisterAsync(request);

            // Assert

            Assert.NotNull(savedUser);

            Assert.NotEqual(request.Password, savedUser.PasswordHash);

            Assert.True(
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    savedUser.PasswordHash));
        }
    }
}
