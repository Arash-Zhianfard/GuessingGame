using Abstraction.Models;

namespace Abstraction.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<Player> AddAsync(Player entity);
        Task<Player> GetAsync(int id);
        public Task<Player?> GetAsync(string username);
        Task<Player> GetAsync(string username, string password);
        Task UpdateAsync(Player entity);
        
    }
}
