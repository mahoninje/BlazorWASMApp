using BlazorWASMApp.Auth.Implementations.Models;

namespace BlazorWASMApp.Auth.Abstract.Services
{
    /// <summary>
    /// Интерфейс сервиса юзеров в local storage
    /// </summary>
    public interface IUserLocalStorageService
    {
        /// <summary>
        /// Логин
        /// </summary>
        /// <returns>IUser в случае успеха, либо null</returns>
        Task<User> LogIn(string login, string password);

        /// <summary>
        /// Добавить объект
        /// </summary>
        void SetItem(string key, object item);

        /// <summary>
        /// Удалить объект
        /// </summary>
        void RemoveItem(string key);

        /// <summary>
        /// Получить юзера по ключу
        /// </summary>
        Task<User> GetUser(string key);

        /// <summary>
        /// Инициализация данных в LocalStorage
        /// </summary>
        void InitLocalStorage();
    }
}
