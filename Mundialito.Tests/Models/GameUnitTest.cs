using System;
using Mundialito.DAL.Games;
using NUnit.Framework;

namespace Mundialito.Tests.Models
{
    [TestFixture]
    public class GameUnitTest
    {
        [Test]
        public void IsOpenSimpleTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime();

            Assert.IsFalse(game.IsOpen(), "Game should not be open");
        }

        [Test]
        public void IsOpenSameDateTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Add(TimeSpan.FromMinutes(35));
            
            Assert.IsTrue(game.IsOpen(), "Game should be open");
        }

        [Test]
        public void IsOpenNotSameDateTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromDays(1)).Add(TimeSpan.FromMinutes(5));

            Assert.IsFalse(game.IsOpen(), "Game should not be open");
        }

        [Test]
        public void IsOpenNotSameDateTestPastTime()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Add(TimeSpan.FromDays(1)).Subtract(TimeSpan.FromMinutes(5));
            
            Assert.IsTrue(game.IsOpen(), "Game shouldb be open");
        }

        [Test]
        public void MarkNotPlayedTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Add(TimeSpan.FromMinutes(35));

            Assert.AreEqual("Not Played", game.Mark());
        }
        
        [Test]
        public void MarkDrawTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(5));

            game.HomeScore = 1;
            game.AwayScore = 1;
            game.CardsMark = "X";
            game.CornersMark = "1";

            Assert.AreEqual("X", game.Mark());
        }

        [Test]
        public void MarkHomeWinTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(5));

            game.HomeScore = 2;
            game.AwayScore = 1;
            game.CardsMark = "X";
            game.CornersMark = "1";

            Assert.AreEqual("1", game.Mark());
        }

        [Test]
        public void MarkAwayWinTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(5));

            game.HomeScore = 1;
            game.AwayScore = 2;
            game.CardsMark = "X";
            game.CornersMark = "1";

            Assert.AreEqual("2", game.Mark());
        }

        [Test]
        public void MarkNotScoreTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(5));

            Assert.AreEqual("Pending Update", game.Mark());
        }
    }
}
