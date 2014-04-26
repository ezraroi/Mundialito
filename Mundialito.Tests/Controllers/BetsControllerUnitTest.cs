using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mundialito.Controllers;
using Mundialito.DAL.Bets;
using Moq;
using Mundialito.Logic;
using Mundialito.Models;
using System.Security.Principal;

namespace Mundialito.Tests.Controllers
{
    [TestClass]
    public class BetsControllerUnitTest
    {
        [TestMethod]
        public void PostBetTest()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(user => user.UserId).Returns("1");
            betsRepository.Setup(rep => rep.InsertBet(It.IsAny<Bet>())).Returns(new Bet() { BetId = 1 });

            var controller = new BetsController(betsRepository.Object, betValidator.Object, userProvider.Object);
            controller.PostBet(new NewBetModel());

            betValidator.Verify(foo => foo.ValidateNewBet(It.IsAny<Bet>()), Times.Exactly(1), "ValidateNewBet must be called");
        }

        [TestMethod]
        public void UpdateBetTest()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(user => user.UserId).Returns("1");

            var controller = new BetsController(betsRepository.Object, betValidator.Object, userProvider.Object);
            controller.UpdateBet(1, new UpdateBetModel() { HomeScore = 1, AwayScore = 2 });

            betValidator.Verify(foo => foo.ValidateUpdateBet(It.IsAny<Bet>()), Times.Exactly(1), "ValidateUpdateBet must be called");
            betsRepository.Verify(rep => rep.UpdateBet(It.IsAny<Bet>()), Times.Exactly(1), "UpdateBet must be called");
        }

        [TestMethod]
        public void DeleteBetTest()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var betValidator = new Mock<IBetValidator>();
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(user => user.UserId).Returns("1");

            var controller = new BetsController(betsRepository.Object, betValidator.Object, userProvider.Object);
            controller.DeleteBet(1);

            betValidator.Verify(foo => foo.ValidateDeleteBet(It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(1), "ValidateDeleteBet must be called");
            betsRepository.Verify(rep => rep.DeleteBet(It.IsAny<int>()), Times.Exactly(1), "DeleteBet must be called");
        }
    }
}
