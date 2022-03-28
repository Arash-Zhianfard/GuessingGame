using Abstraction.Interfaces.Services;

namespace Service.Implementation
{
    public class ScoreService : IScoreService
    {
        public int CalculateGift(int point)
        {
            if (point <= 0)
            {
                throw new ArgumentOutOfRangeException("point should be greater than zero");
            }
            return point * 9;
        }
    }
}
