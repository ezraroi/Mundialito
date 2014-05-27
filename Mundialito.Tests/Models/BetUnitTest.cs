using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Tests.Models
{
    [TestClass]
    public class BetUnitTest
    {
        [TestMethod]
        public void IsOpenSimpleTest()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow } };
            Assert.IsFalse(bet.IsOpenForBetting(), "Bet should not be open");
        }

        [TestMethod]
        public void IsOpenSimpleTest2()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow.AddMinutes(20) } };
            Assert.IsFalse(bet.IsOpenForBetting(), "Bet should not be open");
        }

        [TestMethod]
        public void IsOpenSimpleTest3()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow.AddMinutes(31) } };
            Assert.IsTrue(bet.IsOpenForBetting(), "Bet should be open");
        }

        [TestMethod]
        public void IsResolvedTest()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow } };
            Assert.IsFalse(bet.IsOpenForBetting(), "Bet should not be open");
            Assert.IsFalse(bet.IsResolved(), " Bet should not be resolved");
        }

        [TestMethod]
        public void IsResolvedTest2()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow , HomeScore = 1, AwayScore = 1} };
            Assert.IsFalse(bet.IsOpenForBetting(), "Bet should not be open");
            Assert.IsTrue(bet.IsResolved(), " Bet should be resolved");
        }
    }
}
