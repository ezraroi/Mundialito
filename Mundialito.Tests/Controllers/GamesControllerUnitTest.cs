using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Tests.Controllers
{
    [TestClass]
    public class GamesControllerUnitTest
    {
        private Team homeTeam = new Team() { TeamId = 1, Name = "Team1", ShortName = "TA1" };
        private Team awayTeam = new Team() { TeamId = 1, Name = "Team2", ShortName = "TA2" };
        private Stadium stadium = new Stadium() { StadiumId = 1 };
        private Mock<ILoggedUserProvider> userProvider = new Mock<ILoggedUserProvider>();

        [TestInitialize]
        public void CreateLoggedUserMock()
        {
            userProvider.SetupGet(user => user.UserId).Returns("1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOpenGameBetsTest()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            var openGame = CreateOpenGame(1);
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns(openGame);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            var res = controller.GetGameBets(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void GetNonExistingGameBetsTest()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns((Game)null);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            var res = controller.GetGameBets(1);
        }

        [TestMethod]
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateOpenGameWithResultData()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            var openGame = CreateOpenGame(1);
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns(openGame);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            controller.PutGame(1, new PutGameModel() { HomeScore = 1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void UpdateNonExistingOpenGameWithResultData()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns((Game)null);

            var controller = CreateController(gamesRepository.Object, betsRepository.Object, betsResolver.Object, userProvider.Object, new DateTimeProvider());
            controller.PutGame(1, new PutGameModel() { HomeScore = 1 });
        }

        [TestMethod]
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
            return new GamesController(gamesRepository, betsRepository, betsResolver, userProvider, dateTimeProvider, actionLogsRepository.Object);
        }

    }
}
