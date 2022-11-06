using BlazorWASMApp.Auth.Enums;

namespace BlazorWASMApp.Auth.Abstract.Models
{
    /// <summary>
    /// Интерфейс пользователя
    /// </summary>
    public interface IUser
    {
        public string Login { get; }

        public string Password { get; }

        public UserRole Role { get; }

        /// <summary>
        /// Меняем пароль осознанно
        /// </summary>
        void ChangePassword(string newPassword);
    }
}
