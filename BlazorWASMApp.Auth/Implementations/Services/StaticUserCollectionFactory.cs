using BlazorWASMApp.Auth.Abstract.Services;
using BlazorWASMApp.Auth.Implementations.Models;

namespace BlazorWASMApp.Auth.Implementations.Services
{
    /// <summary>
    /// Статическая фабрика генерации коллекций объектов User
    /// </summary>
    public class StaticUserCollectionFactory : IBaseIUserCollectionFactory
    {
        public Task<IEnumerable<User>> GetUserCollection()
        {
            var userCollection = new User[] 
            {
                new User("user", "user", Enums.UserRole.SimpleUser),
                new User("admin", "admin", Enums.UserRole.Admin)
            };

            return Task.FromResult(userCollection.AsEnumerable());
        }
    }
}
