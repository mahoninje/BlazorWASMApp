using BlazorWASMApp.Auth.Abstract.Services;
using System.Security.Cryptography;
using System.Text;

namespace BlazorWASMApp.Auth.Implementations.Services
{
    /// <summary>
    /// Менеджер паролей (SHA256 хэширование)
    /// </summary>
    public class PasswordHandlerSHA256 : IPasswordHandler
    {
        public string GetHashPassword(string password)
        {
            SHA256 hash = SHA256.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashedPassword);
        }
    }
}
