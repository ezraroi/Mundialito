using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mundialito.Controllers;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.Teams;
using Mundialito.Logic;
using System;
using System.Collections.Generic;
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetOpenGameBetsTest()
        {
            var gamesRepository = new Mock<IGamesRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var betsResolver = new Mock<IBetsResolver>();
            var openGame = CreateOpenGame(1);
            gamesRepository.Setup(rep => rep.GetGame(1)).Returns(openGame);

            var controller = new GamesController(gamesRepository.Object, betsRepository.Object, betsResolver.Object);
            var res = controller.GetGameBets(1);
        }

        private Game CreateOpenGame(int id)
        {
            return new Game() { GameId = id, Date = (DateTime.Now).Add(TimeSpan.FromDays(1)), HomeTeam = homeTeam, AwayTeam = awayTeam };
        }

        private Game CreateClosedGame(int id)
        {
            return new Game() { GameId = id, Date = (DateTime.Now).Subtract(TimeSpan.FromDays(1)), HomeScore = 1, AwayScore = 1, HomeTeam = homeTeam, AwayTeam = awayTeam };
        }

    }
}
