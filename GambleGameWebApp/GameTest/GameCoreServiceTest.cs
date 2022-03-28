using Abstraction.Interfaces.Services;
using Abstraction.Models;
using Moq;
using NUnit.Framework;
using Service.Implementation;
using System;

namespace GameTest
{
    public class GameCoreServiceTest
    {
        private Mock<INumberGeneratorService> _numberGeneratorService;
        private Mock<IPlayerService> _playerService;
        private Mock<IScoreService> _scoreService;
        GameCoreService _gameCoreService;
        [SetUp]
        public void Setup()
        {
            _scoreService = new Mock<IScoreService>();            
            _numberGeneratorService = new Mock<INumberGeneratorService>();
            _playerService = new Mock<IPlayerService>();
            _playerService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Player()
            {
                Id = 1,
                Score = 100,
                UserName ="username",
                Password = "Password",
            });
            _numberGeneratorService.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(8);
            _gameCoreService = new GameCoreService(_playerService.Object, _numberGeneratorService.Object, _scoreService.Object);
        }

        [Test]
        [TestCase(8, true)]
        [TestCase(1, false)]
        public void IsGuessedCorrect_ShouldReturnExpectedResult(int guessedNumber, bool expectResult)
        {
            var result = _gameCoreService.IsGuessedCorrect(guessedNumber);
            Assert.AreEqual(expectResult, result);
            Assert.Pass();
        }
        [Test]
        public void IsGuessedCorrect_ThrowExceptionIfValueIsLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _gameCoreService.IsGuessedCorrect(-1));
        }

        [Test]
        public void Play_ShouldReturnExpectedWinResult()
        {
            var expect = new PlayResult()
            {
                Account = 127,
                Point = "+27",
                Status = PlayStatus.Won.ToString(),
            };
            _scoreService.Setup(x => x.CalculateGift(It.IsAny<int>())).Returns(27);
            _gameCoreService = new GameCoreService(_playerService.Object, _numberGeneratorService.Object, _scoreService.Object);
            var result = _gameCoreService.Play(playerId: 1, guessedNumber: 8, point: 3).Result;
            Assert.AreEqual(expect, result);
        }
        [Test]
        public void Play_ShouldReturnExpectedFailResult()
        {
            var expect = new PlayResult()
            {
                Account = 100,
                Point = "0",
                Status = PlayStatus.Failed.ToString(),
            };
            _scoreService.Setup(x => x.CalculateGift(It.IsAny<int>())).Returns(0);
            _gameCoreService = new GameCoreService(_playerService.Object, _numberGeneratorService.Object, _scoreService.Object);
            var result = _gameCoreService.Play(playerId: 1, guessedNumber: 9, point: 3).Result;
            Assert.AreEqual(expect, result);
        }
    }
}