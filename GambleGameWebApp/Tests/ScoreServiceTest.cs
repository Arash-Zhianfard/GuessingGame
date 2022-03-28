using NUnit.Framework;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ScoreServiceTest
    {
        [Test]
        [TestCase(2, 27)]
        [TestCase(10, 90)]
        [TestCase(5, 45)]
        public void CalcGift_ShouldMultipliedByExpectedNumber(int point, int expectResult)
        {
            var gameCoreService = new ScoreService();
            gameCoreService.CalculateGift(point);
        }
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CalcGift_ThrowExceptionIfValueIsLessThanOrEqualZero(int point)
        {
            var gameCoreService = new ScoreService();
            Assert.Throws<ArgumentOutOfRangeException>(() => gameCoreService.CalculateGift(point));
        }
    }
}
