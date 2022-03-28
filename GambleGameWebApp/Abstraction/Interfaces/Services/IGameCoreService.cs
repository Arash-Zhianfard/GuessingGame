using Abstraction.Models;

namespace Abstraction.Interfaces.Services
{
    public interface IGameCoreService
    {
        Task<PlayResult> Play(int playerId, int guessedNumber, int point);
    }
}
