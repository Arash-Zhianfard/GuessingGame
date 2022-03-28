using Abstraction.Interfaces.Repositories;
using Abstraction.Interfaces.Services;
using Abstraction.Models;

namespace Service.Implementation
{
    public class PlayerService : IPlayerService
    {

        private readonly IPlayerRepository _playerRepository;
        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<Player> AddAsync(Player entity)
        {
            return _playerRepository.CreateAsync(entity);
        }

        public Task<Player> GetAsync(int id)
        {
            return _playerRepository.GetAsync(id);
        }

        public Task<Player?> GetAsync(string username, string password)
        {
            return _playerRepository.GetAsync(username, password);
        }

        public Task<Player?> GetAsync(string username)
        {
            return _playerRepository.GetAsync(username);
        }

        public async Task UpdateAsync(Player entity)
        {
            await _playerRepository.UpdateAsync(entity);
        }
    }
}
