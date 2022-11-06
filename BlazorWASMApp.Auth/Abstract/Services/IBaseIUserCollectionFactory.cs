using BlazorWASMApp.Auth.Implementations.Models;

namespace BlazorWASMApp.Auth.Abstract.Services
{
    /// <summary>
    /// Интерфейс фабрики генерации коллекций объектов User
    /// </summary>
    public interface IBaseIUserCollectionFactory
    {
        /// <summary>
        /// Получить коллекцию User
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUserCollection();
    }
}
