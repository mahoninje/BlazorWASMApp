using BlazorWASMApp.Auth.Abstract.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorWASMApp.Auth.Implementations.Services
{
    /// <summary>
    /// Провайдер состояния аутентификации
    /// </summary>
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly IUserLocalStorageService _localStorageService;

        /// <summary>
        /// Ключ успешного логина - "loggeduserkey"
        /// </summary>
        public static readonly string LoggedUserKey = "loggeduserkey";

        public CustomAuthStateProvider(IUserLocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var loggedUser = await _localStorageService.GetUser(LoggedUserKey);
            if (loggedUser == null)
                return new AuthenticationState(_anonymous);
            else 
            {
                var claims = new Claim[]
                {
                    new(ClaimTypes.Name, loggedUser.Login),
                    new("password", loggedUser.Password),
                    new(ClaimTypes.Role, loggedUser.Role.ToString())
                };

                var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "JwtAuth")));

                return state;
            }
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        public void LogOut() 
        {
            _localStorageService.RemoveItem(LoggedUserKey);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}
