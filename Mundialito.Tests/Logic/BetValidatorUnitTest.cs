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

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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
            CreateClosedGame(gamesRepository);

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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
            var pendingUpdateGame = new Mock<IGame>();
            pendingUpdateGame.SetupGet(game => game.IsPendingUpdate).Returns(true);
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns(pendingUpdateGame.Object);

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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
            CreateOpenGame(gamesRepository);

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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
            CreateOpenGame(gamesRepository);

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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
            CreateClosedGame(gamesRepository);

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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
            
            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
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

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
            betValidator.ValidateDeleteBet(1, "");
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void TestDeleteBetOtherUser()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns(new Bet() { BetId = 1, User = new MundialitoUser() { Id = "2" } });

            var gamesRepository = new Mock<IGamesRepository>();

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
            betValidator.ValidateDeleteBet(1, "1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteBetClosedGame()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetBet(It.IsAny<int>())).Returns(new Bet() { BetId = 1, User = new MundialitoUser() { Id = "1" }, Game = new Game() { Date = DateTime.Now.Subtract(TimeSpan.FromDays(1)) } });

            var gamesRepository = new Mock<IGamesRepository>();
            CreateClosedGame(gamesRepository);

            var betValidator = new BetValidator(gamesRepository.Object, betsRepository.Object);
            betValidator.ValidateDeleteBet(1, "1");
        }

        private static void CreateClosedGame(Mock<IGamesRepository> gamesRepository)
        {
            var closedGame = new Mock<IGame>();
            closedGame.SetupGet(game => game.IsOpen).Returns(false);
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns(closedGame.Object);
        }

        private static void CreateOpenGame(Mock<IGamesRepository> gamesRepository)
        {
            var game = new Mock<IGame>();
            game.SetupGet(g => g.IsPendingUpdate).Returns(false);
            game.SetupGet(g => g.IsOpen).Returns(true);
            gamesRepository.Setup(rep => rep.GetGame(It.IsAny<int>())).Returns(game.Object);
        }

        
    }
}
