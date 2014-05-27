using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mundialito.Logic;
using Moq;
using Mundialito.DAL.Games;
using Mundialito.DAL.Bets;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Mundialito.DAL.Accounts;

namespace Mundialito.Tests.Logic
{
    [TestClass]
    public class BetValidatorUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNewBetNonExistingGame()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var gamesRepository = new Mock<IGamesRepository>();
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns((Game)null);

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var newBet = new Bet();
            newBet.Game = new Game();
            newBet.Game.GameId = 1;
            betValidator.ValidateNewBet(newBet);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNewBetClosedGame()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var gamesRepository = new Mock<IGamesRepository>();
            CreateClosedGame();

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var newBet = new Bet();
            newBet.Game = new Game();
            newBet.Game.GameId = 1;
            betValidator.ValidateNewBet(newBet);
        }
             

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNewBetPendingUpdateGame()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var gamesRepository = new Mock<IGamesRepository>();
            var game = CreatePendingUpdateGame();
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns(game);

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var newBet = new Bet();
            newBet.Game = new Game();
            newBet.Game.GameId = 1;
            betValidator.ValidateNewBet(newBet);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNewBetAlreadyExistingBet()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetGameBets(It.IsAny<int>())).Returns(new List<Bet> { new Bet() { BetId = 1, User = new MundialitoUser() { Id = "1"} } });

            var gamesRepository = new Mock<IGamesRepository>();
            var game = CreateOpenGame();
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns(game);;

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var newBet = new Bet();
            newBet.Game = new Game();
            newBet.Game.GameId = 1;
            newBet.User = new MundialitoUser() { Id = "1" };
            betValidator.ValidateNewBet(newBet);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNewBetWithoutUserBet()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetGameBets(It.IsAny<int>())).Returns(new List<Bet> { new Bet() { BetId = 1, User = new MundialitoUser() { Id = "1" } } });

            var gamesRepository = new Mock<IGamesRepository>();
            var game = CreateOpenGame();
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns(game); ;

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var newBet = new Bet();
            newBet.Game = new Game();
            newBet.Game.GameId = 1;
            betValidator.ValidateNewBet(newBet);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBetNonExistingBet()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns((Bet)null);

            var gamesRepository = new Mock<IGamesRepository>();

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var betToUpdate = new Bet();
            betToUpdate.BetId = 1;
            betValidator.ValidateUpdateBet(betToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBetClosedGame()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns(new Bet() { BetId = 1 });

            var gamesRepository = new Mock<IGamesRepository>();
            var game = CreateClosedGame();
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns(game); ;

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var betToUpdate = new Bet();
            betToUpdate.BetId = 1;
            betToUpdate.Game = new Game();
            betToUpdate.Game.GameId = 1;
            betValidator.ValidateUpdateBet(betToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void TestUpdateBetNotMineBet()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns(new Bet() { BetId = 1, User = new MundialitoUser() { Id = "2"  }});

            var gamesRepository = new Mock<IGamesRepository>();

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var betToUpdate = new Bet();
            betToUpdate.BetId = 1;
            betToUpdate.Game = new Game();
            betToUpdate.Game.GameId = 1;
            betToUpdate.User = new MundialitoUser() { Id = "1" };
            betValidator.ValidateUpdateBet(betToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBetWithoutUserBet()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns(new Bet() { BetId = 1, User = new MundialitoUser() { Id = "2" } });

            var gamesRepository = new Mock<IGamesRepository>();

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            var betToUpdate = new Bet();
            betToUpdate.BetId = 1;
            betValidator.ValidateUpdateBet(betToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteBetNotExists()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns((Bet)null);

            var gamesRepository = new Mock<IGamesRepository>();

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            betValidator.ValidateDeleteBet(1, "");
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void TestDeleteBetOtherUser()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns(new Bet() { BetId = 1, User = new MundialitoUser() { Id = "2" } });

            var gamesRepository = new Mock<IGamesRepository>();

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            betValidator.ValidateDeleteBet(1, "1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteBetClosedGame()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns(new Bet() { BetId = 1, User = new MundialitoUser() { Id = "1" }, Game = new Game() { Date = DateTime.Now.Subtract(TimeSpan.FromDays(1)) } });

            var gamesRepository = new Mock<IGamesRepository>();
            var game = CreateClosedGame();
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns(game); ;

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object, new DateTimeProvider());
            betValidator.ValidateDeleteBet(1, "1");
        }

        private static Game CreateClosedGame()
        {
            var closedGame = new Game();
            closedGame.Date = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            closedGame.HomeScore = closedGame.AwayScore = 1;
            return closedGame;
        }

        private static Game CreateOpenGame()
        {
            var game = new Game();
            game.Date = DateTime.Now.Add(TimeSpan.FromDays(1));
            return game;
        }

        private static Game CreatePendingUpdateGame()
        {
            var pendingUpdateGame = new Game();
            pendingUpdateGame.Date = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            return pendingUpdateGame;
        }

    }
}
