using BlazorWASMApp.Auth.Abstract.Services;
using BlazorWASMApp.Auth.Implementations.Services;
using BlazorWASMApp.Web.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorWASMApp.Web.Pages
{
    public class LoginModel : ComponentBase
    {
        [Inject] public IUserLocalStorageService LocalStorageService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        public LoginDto LoginData { get; set; }

        public LoginModel()
        {
            LoginData = new();
        }

        protected async Task LoginAsync() 
        {
            if (!ValidLoginData()) 
            {
                FailLogin();
                return;
            } 

            var foundUser = await LocalStorageService.LogIn(LoginData.Login, LoginData.Password);
            if (foundUser == null)
            {
                FailLogin();
                return;
            }

            LocalStorageService.SetItem(CustomAuthStateProvider.LoggedUserKey, foundUser);

            ErrorMessage = string.Empty;
            NavigationManager.NavigateTo("/", true);
        }

        private bool ValidLoginData() =>
            LoginData != null &&
            !string.IsNullOrEmpty(LoginData.Login) &&
            !string.IsNullOrEmpty(LoginData.Password);

        private void FailLogin() 
        {
            ErrorMessage = "Invalid login or password!";
        }
    }
}
