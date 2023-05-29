using System; 
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    public interface ICache
    {
        Task<T> Get<T>(string key);

        Task Set<T>(string key, T value);

        Task Set<T>(string key, T value, DateTimeOffset absoluteExpiration);

        Task Remove(string key);

        //Task<List<string>> GetAllKeys();
    }
}