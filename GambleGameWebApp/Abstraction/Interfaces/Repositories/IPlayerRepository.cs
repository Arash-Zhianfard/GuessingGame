using Abstraction.Models;

namespace Abstraction.Interfaces.Repositories
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Task<Player?> GetAsync(string username);
        Task<Player?> GetAsync(string username, string password);
    }
}
