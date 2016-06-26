using System;
using System.Linq;
using Mundialito.Controllers;
using Mundialito.DAL.Bets;
using Moq;
using Mundialito.Logic;
using Mundialito.Models;
using System.Security.Principal;
using System.Collections.Generic;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Games;
using Mundialito.DAL.Teams;
using System.Data.Entity.Core;
using Mundialito.DAL.ActionLogs;
using NUnit.Framework;

namespace Mundialito.Tests.Controllers
{
    [TestFixture]
    public class BetsControllerUnitTest
    {
        private Team homeTeam = new Team() { TeamId = 1, Name = "Team1", ShortName = "TA1" };
        private Team awayTeam = new Team() { TeamId = 1, Name = "Team2", ShortName = "TA2" };

        [Test]
        public void PostBetTest()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(user => user.UserId).Returns("1");
            betsRepository.Setup(rep => rep.InsertBet(It.IsAny<Bet>())).Returns(new Bet() { BetId = 1 });

            var controller = CreateController(betsRepository.Object, betValidator.Object, userProvider.Object, new DateTimeProvider());
            controller.PostBet(new NewBetModel());

            betValidator.Verify(foo => foo.ValidateNewBet(It.IsAny<Bet>()), Times.Exactly(1), "ValidateNewBet must be called");
        }

        [Test]
        public void GetNonExistingBetByIdTest()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            betsRepository.Setup(rep => rep.GetBet(1)).Returns((Bet)null);

            var controller = CreateController(betsRepository.Object, betValidator.Object, userProvider.Object, new DateTimeProvider());
            Assert.Throws<ObjectNotFoundException>(() => controller.GetBetById(1));
        }

        [Test]
        public void GetBetByIdTest()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            var bet = new Bet()
            {
                BetId = 1,
                User = new MundialitoUser() { Id = "1", UserName = "ezraroi" },
                Game = new Game() { GameId = 1, HomeTeam = homeTeam, AwayTeam = awayTeam, Date = DateTime.Now.ToUniversalTime() },
                HomeScore = 1,
                AwayScore = 1,
                CardsMark = "X",
                CornersMark = "1",
            };
            betsRepository.Setup(rep => rep.GetBet(1)).Returns(bet);

            var controller = CreateController(betsRepository.Object, betValidator.Object, userProvider.Object, new DateTimeProvider());
            var res = controller.GetBetById(1);
            Assert.AreEqual(1, res.BetId);
            Assert.AreEqual("X", res.CardsMark);
            Assert.AreEqual("1", res.CornersMark);
            Assert.AreEqual(1, res.HomeScore);
            Assert.AreEqual(1, res.AwayScore);
        }

        [Test]
        public void GetLoggedUserBets()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            List<Bet> bets = new List<Bet>();
            userProvider.SetupGet(user => user.UserName).Returns("ezraroi");
            betsRepository.Setup(rep => rep.GetUserBets("ezraroi")).Returns(new List<Bet> { 
                new Bet() { 
                    BetId = 1, User = new MundialitoUser() { Id = "1" , UserName = "ezraroi"} , Game = new Game() { GameId = 1, HomeTeam = homeTeam, AwayTeam = awayTeam, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 1, AwayScore = 1 , CardsMark = "X", CornersMark = "1", 
                },
                new Bet() { 
                    BetId = 4, User = new MundialitoUser() { Id = "1" , UserName = "ezraroi"} , Game = new Game() { GameId = 2, HomeTeam = homeTeam, AwayTeam = awayTeam, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 2, AwayScore = 2 , CardsMark = "X", CornersMark = "2"
                },
                new Bet() { 
                    BetId = 5, User = new MundialitoUser() { Id = "1" , UserName = "ezraroi"} , Game = new Game() { GameId = 2, HomeTeam = homeTeam, AwayTeam = awayTeam,Date = DateTime.Now.ToUniversalTime().AddDays(2)}  }
            });

            var controller = CreateController(betsRepository.Object, betValidator.Object, userProvider.Object, new DateTimeProvider());
            var res = controller.GetUserBets("ezraroi");
            Assert.AreEqual(3, res.Count());
        }

        [Test]
        public void GetUserBets()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            List<Bet> bets = new List<Bet>();
            userProvider.SetupGet(user => user.UserName).Returns("admin");
            betsRepository.Setup(rep => rep.GetUserBets("ezraroi")).Returns(new List<Bet> { 
                new Bet() { 
                    BetId = 1, User = new MundialitoUser() { Id = "1" , UserName = "ezraroi"} , Game = new Game() { GameId = 1, HomeTeam = homeTeam, AwayTeam = awayTeam, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 1, AwayScore = 1 , CardsMark = "X", CornersMark = "1", 
                },
                new Bet() { 
                    BetId = 4, User = new MundialitoUser() { Id = "1" , UserName = "ezraroi"} , Game = new Game() { GameId = 2, HomeTeam = homeTeam, AwayTeam = awayTeam, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 2, AwayScore = 2 , CardsMark = "X", CornersMark = "2"
                },
                new Bet() { 
                    BetId = 5, User = new MundialitoUser() { Id = "1" , UserName = "ezraroi"} , Game = new Game() { GameId = 2, HomeTeam = homeTeam, AwayTeam = awayTeam,Date = DateTime.Now.ToUniversalTime().AddDays(2)}  }
            });


            var controller = CreateController(betsRepository.Object, betValidator.Object, userProvider.Object, new DateTimeProvider());
            var res = controller.GetUserBets("ezraroi");
            Assert.AreEqual(2, res.Count());
            bets.ForEach(bet => Assert.IsFalse(bet.IsOpenForBetting()));
        }

        [Test]
        public void UpdateBetTest()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(user => user.UserId).Returns("1");

            var controller = CreateController(betsRepository.Object, betValidator.Object, userProvider.Object, new DateTimeProvider());
            controller.UpdateBet(1, new UpdateBetModel() { HomeScore = 1, AwayScore = 2 });

            betValidator.Verify(foo => foo.ValidateUpdateBet(It.IsAny<Bet>()), Times.Exactly(1), "ValidateUpdateBet must be called");
            betsRepository.Verify(rep => rep.UpdateBet(It.IsAny<Bet>()), Times.Exactly(1), "UpdateBet must be called");
        }

        [Test]
        public void DeleteBetTest()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(user => user.UserId).Returns("1");

            var controller = CreateController(betsRepository.Object, betValidator.Object, userProvider.Object, new DateTimeProvider());
            controller.DeleteBet(1);

            betValidator.Verify(foo => foo.ValidateDeleteBet(It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(1), "ValidateDeleteBet must be called");
            betsRepository.Verify(rep => rep.DeleteBet(It.IsAny<int>()), Times.Exactly(1), "DeleteBet must be called");
        }

        private BetsController CreateController(IBetsRepository betsRepository, IBetValidator betValidator, ILoggedUserProvider userProvider, IDateTimeProvider dateTimeProvider)
        {
            var actionLogsRepository = new Mock<IActionLogsRepository>();
            var gamesRepository = new Mock<IGamesRepository>();
            return new BetsController(betsRepository, betValidator, userProvider, dateTimeProvider, actionLogsRepository.Object, gamesRepository.Object);
        }
    }
}
