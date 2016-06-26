using Moq;
using Mundialito.Controllers;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.ActionLogs;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using Mundialito.Logic;
using Mundialito.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace Mundialito.Tests.Controllers
{
    [TestFixture]
    public class GamesControllerUnitTest
    {
        private Team homeTeam = new Team() { TeamId = 1, Name = "Team1", ShortName = "TA1" };
        private Team awayTeam = new Team() { TeamId = 1, Name = "Team2", ShortName = "TA2" };
        private Stadium stadium = new Stadium() { StadiumId = 1 };
        private Mock<ILoggedUserProvider> userProvider = new Mock<ILoggedUserProvider>();

        [SetUp]
        public void CreateLoggedUserMock()
        {
            userProvider.SetupGet(user => user.UserId).Returns("1");
        }

        [Test]
        public void GetOpenGameBetsTest()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            var openGame = CreateOpenGame(1);
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns(openGame);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            Assert.Throws<ArgumentException>(() => controller.GetGameBets(1));
        }

        [Test]
        public void GetNonExistingGameBetsTest()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns((Game)null);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            Assert.Throws<ObjectNotFoundException>(() => controller.GetGameBets(1));
        }

        [Test]
        public void GetClosedGameBetsTest()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            var closedGame = CreateClosedGame(1);
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns(closedGame);
            List<Bet> bets = new List<Bet>();
            bets.Add(new Bet() { BetId = 1, Game = closedGame, User = new MundialitoUser() { FirstName = "A", LastName = "B", UserName = "User" } });
            betsRepository.Setup(rep => rep.GetGameBets(1)).Returns(bets);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            var res = controller.GetGameBets(1);
            Assert.AreEqual(1, res.Count());
        }

        [Test]
        public void UpdateOpenGameWithResultData()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            var openGame = CreateOpenGame(1);
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns(openGame);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            Assert.Throws<ArgumentException>(() => controller.PutGame(1, new PutGameModel() { HomeScore = 1 }));
        }

        [Test]
        public void UpdateNonExistingOpenGameWithResultData()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns((Game)null);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            Assert.Throws<ObjectNotFoundException>(() => controller.PutGame(1, new PutGameModel() { HomeScore = 1 }));
        }

        [Test]
        public void UpdateClosedGameWithResultData()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            var closedGame = CreateClosedGame(1);
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns(closedGame);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            controller.PutGame(1, new PutGameModel() {HomeScore = 1, AwayScore = 1, CardsMark = "X", CornersMark = "1", Date = closedGame.Date });
            betsResolver.Verify(item => item.ResolveBets(closedGame));
        }

        private Game CreateOpenGame(int id)
        {
            return new Game() { GameId = id, Date = (DateTime.UtcNow).Add(TimeSpan.FromDays(1)), HomeTeam = homeTeam, AwayTeam = awayTeam ,Stadium = stadium };
        }

        private Game CreateClosedGame(int id)
        {
            return new Game() { GameId = id, Date = (DateTime.UtcNow).Subtract(TimeSpan.FromDays(1)), HomeScore = 1, AwayScore = 1, HomeTeam = homeTeam, AwayTeam = awayTeam , Stadium = stadium};
        }

        private GamesController CreateController(IGamesRepository gamesRepository, IBetsRepository betsRepository, IBetsResolver betsResolver, ILoggedUserProvider userProvider, IDateTimeProvider dateTimeProvider)
        {
            var actionLogsRepository = new Mock<IActionLogsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            return new GamesController(gamesRepository, betsRepository, betsResolver, userProvider, dateTimeProvider, usersRepository.Object, actionLogsRepository.Object);
        }

    }
}
