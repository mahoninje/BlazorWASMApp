using Blazored.LocalStorage;
using BlazorWASMApp.Auth.Abstract.Services;
using BlazorWASMApp.Auth.Implementations.Services;
using BlazorWASMApp.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorWASMApp.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IBaseIUserCollectionFactory, StaticUserCollectionFactory>();
            builder.Services.AddScoped<IPasswordHandler, PasswordHandlerSHA256>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IUserLocalStorageService, BaseUserLocalStorageService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddScoped<CustomAuthStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}