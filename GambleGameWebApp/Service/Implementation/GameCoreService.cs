using Abstraction.Interfaces.Services;
using Abstraction.Models;

namespace Service.Implementation
{
    public class GameCoreService : IGameCoreService
    {
        private readonly IPlayerService _player;
        private readonly IScoreService _scoreService;
        private readonly INumberGeneratorService _numberGeneratorService;
        public GameCoreService(IPlayerService player,
            INumberGeneratorService numberGeneratorService, IScoreService scoreService)
        {
            _numberGeneratorService = numberGeneratorService;
            _player = player;
            _scoreService = scoreService;
        }

        public bool IsGuessedCorrect(int guessedNumber)
        {
            if (guessedNumber < 0)
            {
                throw new ArgumentOutOfRangeException("guessedNumber should be greater than zero");
            }
            var rndNumber = _numberGeneratorService.Generate(0, 9);
            if (rndNumber == guessedNumber)
            {
                return true;
            }
            return false;
        }

        public async Task<PlayResult> Play(int playerId, int guessedNumber, int point)
        {
            var player = await _player.GetAsync(playerId);
            if (IsGuessedCorrect(guessedNumber))
            {

                var newScore = _scoreService.CalculateGift(point);
                player.Score += newScore;
                await _player.UpdateAsync(player);
                return new PlayResult()
                {
                    Account = player.Score,
                    Point = $"+{newScore}",
                    Status = PlayStatus.Won.ToString(),
                };
            }
            return new PlayResult()
            {
                Account = player.Score,
                Point = "0",
                Status = PlayStatus.Failed.ToString(),
            };  
        }
    }
}
