using Blazored.LocalStorage;
using BlazorWASMApp.Auth.Abstract.Services;
using BlazorWASMApp.Auth.Implementations.Models;

namespace BlazorWASMApp.Auth.Implementations.Services
{
    /// <summary>
    /// Базовый сервис по работе с LocalStorage
    /// </summary>
    public class BaseUserLocalStorageService : IUserLocalStorageService
    {
        /// <summary>
        /// Фабрика генерации User коллекций
        /// </summary>
        private readonly IBaseIUserCollectionFactory _userCollectionFactory;
        /// <summary>
        /// Сервис LocalStorage из библиотеки Blazored.LocalStorage
        /// </summary>
        private readonly ILocalStorageService _localStorageService;
        /// <summary>
        /// Менеджер паролей
        /// </summary>
        private readonly IPasswordHandler _passwordHandler;

        public BaseUserLocalStorageService(
            IBaseIUserCollectionFactory userCollectionFactory, 
            ILocalStorageService localStorageService,
            IPasswordHandler passwordHandler)
        {
            _userCollectionFactory = userCollectionFactory;
            _localStorageService = localStorageService;
            _passwordHandler = passwordHandler;
        }

        
        /// <summary>
        /// Логин
        /// </summary>
        /// <returns>IUser в случае успеха, либо null</returns>
        public async Task<User> LogIn(string login, string password) 
        {
            var user = await GetUser(login);
            if (user == null) return null;

            if (!user.Password.Equals(_passwordHandler.GetHashPassword(password)))
                return null;
            
            return user;
        }

        /// <summary>
        /// Добавить объект
        /// </summary>
        public void SetItem(string key, object item) 
        {
            if (string.IsNullOrEmpty(key) || item == null) return;

            _localStorageService.SetItemAsync(key.ToLower(), item);
        }

        /// <summary>
        /// Удалить объект
        /// </summary>
        public void RemoveItem(string key) 
        {
            if (string.IsNullOrEmpty(key)) return;

            _localStorageService.RemoveItemAsync(key.ToLower());
        }

        /// <summary>
        /// Получить юзера по ключу
        /// </summary>
        public async Task<User> GetUser(string key)
        {
            return await _localStorageService.GetItemAsync<User>(key.ToLower());
        }

        /// <summary>
        /// Инициализация данных в LocalStorage
        /// </summary>
        public void InitLocalStorage() 
        {
            var userCollection = _userCollectionFactory.GetUserCollection().Result;
            _localStorageService.ClearAsync();

            foreach (var user in userCollection) 
            {
                var hashedPassword = _passwordHandler.GetHashPassword(user.Password);
                user.ChangePassword(hashedPassword);

                SetItem(user.Login, user);
            }
        }
    }
}
