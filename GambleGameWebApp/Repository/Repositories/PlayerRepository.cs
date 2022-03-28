using Abstraction.Interfaces.Repositories;
using Abstraction.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.BaseRepository;

namespace Arash.Zhianfard.Application.Data.Repositories
{

    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        private readonly AppDbContext _appDbContext;
        public PlayerRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }
        public Task<Player?> GetAsync(string username, string password)
        {
            return _appDbContext.Players.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);
        }
        public Task<Player?> GetAsync(string username)
        {
            return _appDbContext.Players.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());

        }
    }
}
