using Abstraction.Interfaces.Services;

namespace Service.Implementation
{
    public class NumberGeneratorService : INumberGeneratorService
    {
        public int Generate(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
