using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using LoveLetters.Service.Helpers;
using LoveLetters.Service.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;

namespace LoveLetters.Test.Services.Auth
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<IAuthRepository> mockAuthRepository;
        private Mock<IConfiguration> mockConfiguration;
        private AuthService authService;

        private readonly string secretkey = "11111111-1111-1111-1111-111111111111";
        private readonly string salt = "01234567890123456";

        [SetUp]
        public void Setup()
        {
            mockAuthRepository = new Mock<IAuthRepository>();
            mockConfiguration = new Mock<IConfiguration>();

            // Configurando a chave secreta e o Salt
            mockConfiguration.Setup(c => c["SecretKey"]).Returns(secretkey);
            mockConfiguration.Setup(c => c["Salt"]).Returns(salt);

            authService = new AuthService(mockAuthRepository.Object, mockConfiguration.Object);
        }

        [Test]
        public async Task LoginUser_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var noHashedPassword = "password123";

            var user = new Users
            {
                guid = Guid.NewGuid().ToString(),
                name = "UserTest",
                email = "test@example.com",
                password = new PasswordHelper(salt).HashPassword(noHashedPassword),
                havePartner = true
            };

            mockAuthRepository.Setup(repo => repo.GetUserByEmail(user.email))
                .ReturnsAsync(user);

            // Act
            var result = await authService.LoginUser(user.email, noHashedPassword);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Code == 200);
            Assert.That(result.Message == "Login efetuado com sucesso!");
        }

        [Test]
        public async Task LoginUser_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            var noHashedPassword = "password123";

            var user = new Users
            {
                guid = Guid.NewGuid().ToString(),
                name = "UserTest",
                email = "test@example.com",
                password = new PasswordHelper(salt).HashPassword(noHashedPassword),
                havePartner = true
            };

            mockAuthRepository.Setup(repo => repo.GetUserByEmail(user.email))
                .ReturnsAsync((Users)null); // Retorna null para simular usuário não encontrado

            // Act
            var result = await authService.LoginUser(user.email, user.password);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(403 == result.Code);
            Assert.That("Email e/ou senha incorreta!" == result.Message);
        }

        [Test]
        public async Task LoginUser_ShouldReturnError_WhenPasswordIsIncorrect()
        {
            // Arrange
            var noHashedPassword = "password123";
            var incorrectPassword = "nopassword123";

            var user = new Users
            {
                guid = Guid.NewGuid().ToString(),
                name = "UserTest",
                email = "test@example.com",
                password = new PasswordHelper(salt).HashPassword(incorrectPassword),
                havePartner = true
            };

            mockAuthRepository.Setup(repo => repo.GetUserByEmail(user.email))
                .ReturnsAsync(user);

            // Act
            var result = await authService.LoginUser(user.email, noHashedPassword);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Code == 403);
            Assert.That(result.Message == "Email e/ou senha incorreta!");
        }

        [Test]
        public async Task RegisterUser_ShouldReturnSuccess_WhenCredentialsIsValid()
        {
            // Arrange
            var requestUser = new Users
            {
                guid = "",
                name = "NewUserTest",
                email = "test@example.com",
                password = "password123",
                havePartner = false
            };

            var userReturnedByGetUsersByEmail = new Users
            {
                guid = "",
                name = "NewUserTest",
                email = "test@example.com",
                password = new PasswordHelper(salt).HashPassword(requestUser.password),
                havePartner = false
            };

            mockAuthRepository.Setup(repo => repo.GetUserByEmail(requestUser.email))
                .ReturnsAsync(userReturnedByGetUsersByEmail);

            // Act
            var result = await authService.RegisterUser(requestUser);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(200 == result.Code);
            Assert.That(result.Message == "Email e/ou senha incorreta!");
        }
    }
}
