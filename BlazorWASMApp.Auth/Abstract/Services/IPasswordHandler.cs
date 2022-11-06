
namespace BlazorWASMApp.Auth.Abstract.Services
{
    /// <summary>
    /// Интерфейс менеджера паролей
    /// </summary>
    public interface IPasswordHandler
    {
        /// <summary>
        /// Получить хэш пароля
        /// </summary>
        string GetHashPassword(string password);
    }
}
