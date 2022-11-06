using Blazored.LocalStorage;
using BlazorWASMApp.Auth.Abstract.Models;
using BlazorWASMApp.Auth.Abstract.Services;
using BlazorWASMApp.Auth.Implementations.Models;
using BlazorWASMApp.Auth.Implementations.Services;
using Moq;

namespace BlazorWASMApp.AuthTests
{
    public class BaseLocalStorageServiceTests
    {
        private readonly BaseUserLocalStorageService _sut;
        private readonly Mock<IBaseIUserCollectionFactory> _userCollectionFactoryMock = new();
        private readonly Mock<ILocalStorageService> _localStorageServiceMock = new();
        private readonly Mock<IPasswordHandler> _passwordHandlerMock = new();

        public BaseLocalStorageServiceTests()
        {
            _sut = new BaseUserLocalStorageService(
                _userCollectionFactoryMock.Object,
                _localStorageServiceMock.Object,
                _passwordHandlerMock.Object);
        }

        [Fact]
        public async Task LogIn_ShouldReturnUser_With_Proper_Credentials()
        {
            // Arrange
            var user = new User("TestUser1", "qwerty123", Auth.Enums.UserRole.SimpleUser);
            _localStorageServiceMock.Setup(x => x.GetItemAsync<User>(user.Login.ToLower(), null)).ReturnsAsync(user);
            _passwordHandlerMock.Setup(x => x.GetHashPassword(user.Password)).Returns(user.Password);

            // Act
            var result = await _sut.LogIn(user.Login, user.Password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Login, result.Login);
        }

        [Fact]
        public async Task LogIn_ShouldReturnNull_If_User_Doesnt_Exists()
        {
            // Arrange
            var user = new User("TestUser1", "qwerty123", Auth.Enums.UserRole.SimpleUser);
            User notFountUser = null;
            _localStorageServiceMock.Setup(x => x.GetItemAsync<User>(user.Login.ToLower(), null)).ReturnsAsync(notFountUser);

            // Act
            var result = await _sut.LogIn(user.Login, user.Password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LogIn_ShouldReturnNull_With_Invalid_Password()
        {
            // Arrange
            var user = new User("TestUser1", "qwerty123", Auth.Enums.UserRole.SimpleUser);
            var differentPassword = "someDifferentPass";
            _localStorageServiceMock.Setup(x => x.GetItemAsync<User>(user.Login.ToLower(), null)).ReturnsAsync(user);
            _passwordHandlerMock.Setup(x => x.GetHashPassword(user.Password)).Returns(differentPassword);

            // Act
            var result = await _sut.LogIn(user.Login, user.Password);

            // Assert
            Assert.Null(result);
        }
    }
}