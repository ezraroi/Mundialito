using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using NUnit.Framework;
using System;

namespace Mundialito.Tests.Models
{
    [TestFixture]
    public class BetUnitTest
    {
        [Test]
        public void IsOpenSimpleTest()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow } };
            Assert.IsFalse(bet.IsOpenForBetting(), "Bet should not be open");
        }

        [Test]
        public void IsOpenSimpleTest2()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow.AddMinutes(20) } };
            Assert.IsFalse(bet.IsOpenForBetting(), "Bet should not be open");
        }

        [Test]
        public void IsOpenSimpleTest3()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow.AddMinutes(31) } };
            Assert.IsTrue(bet.IsOpenForBetting(), "Bet should be open");
        }

        [Test]
        public void IsResolvedTest()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow } };
            Assert.IsFalse(bet.IsOpenForBetting(), "Bet should not be open");
            Assert.IsFalse(bet.IsResolved(), " Bet should not be resolved");
        }

        [Test]
        public void IsResolvedTest2()
        {
            var bet = new Bet() { Game = new Game() { Date = DateTime.UtcNow , HomeScore = 1, AwayScore = 1, CornersMark = "X", CardsMark = "1"} };
            Assert.IsFalse(bet.IsOpenForBetting(), "Bet should not be open");
            Assert.IsTrue(bet.IsResolved(), " Bet should be resolved");
        }
    }
}
