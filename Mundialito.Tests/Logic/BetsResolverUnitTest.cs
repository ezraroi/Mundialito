using Moq;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.ActionLogs;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Mundialito.Tests.Logic
{
    [TestFixture]
    public class BetsResolverUnitTest
    {
        [Test]
        public void TestUnResolvedGame()
        {

            var betsRepository = new Mock<IBetsRepository>();
            var resolver = CreateTarget(betsRepository.Object, new DateTimeProvider());
            Assert.Throws<ArgumentException>(() => resolver.ResolveBets(new Game()
            {
                GameId = 1,
                Date = new DateTime().AddDays(2)
            }));
        }

        [Test]
        public void TestBetResolving()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetGameBets(It.IsAny<int>())).Returns(new List<Bet> { 
                new Bet() { 
                    BetId = 1, User = new MundialitoUser() { Id = "1" } , Game = new Game() { GameId = 1, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 1, AwayScore = 1 , CardsMark = "X", CornersMark = "1"
                },
                new Bet() { 
                    BetId = 2, User = new MundialitoUser() { Id = "2" } , Game = new Game() { GameId = 1, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 2, AwayScore = 1 , CardsMark = "2", CornersMark = "1"
                },
                new Bet() { 
                    BetId = 3, User = new MundialitoUser() { Id = "3" } , Game = new Game() { GameId = 1, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 2, AwayScore = 2 , CardsMark = "1", CornersMark = "X"
                },
                new Bet() { 
                    BetId = 4, User = new MundialitoUser() { Id = "4" } , Game = new Game() { GameId = 2, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 2, AwayScore = 2 , CardsMark = "X", CornersMark = "2"
                }
            });
            var resolver = CreateTarget(betsRepository.Object, new DateTimeProvider());
            resolver.ResolveBets(new Game()
            {
                GameId = 1,
                Date = DateTime.Now.ToUniversalTime(),
                HomeScore = 1,
                AwayScore = 1,
                CardsMark = "X",
                CornersMark = "1"
            });
            betsRepository.Verify(res => res.UpdateBet(It.Is<Bet>(bet => bet.BetId == 1 && bet.Points == 7 && bet.GameMarkWin && bet.ResultWin && bet.CornersWin && bet.CardsWin)));
            betsRepository.Verify(res => res.UpdateBet(It.Is<Bet>(bet => bet.BetId == 2 && bet.Points == 1 && !bet.GameMarkWin && !bet.ResultWin && bet.CornersWin && !bet.CardsWin)));
            betsRepository.Verify(res => res.UpdateBet(It.Is<Bet>(bet => bet.BetId == 3 && bet.Points == 3 && bet.GameMarkWin && !bet.ResultWin && !bet.CornersWin && !bet.CardsWin)));
            betsRepository.Setup(res => res.UpdateBet(It.Is<Bet>(bet => bet.BetId == 4))).Throws(new Exception("Should not be called"));
        }

        [Test]
        public void TestBetResolvingGameWithNoBets()
        {
            var betsRepository = new Mock<IBetsRepository>();
            betsRepository.Setup(res => res.GetGameBets(It.IsAny<int>())).Returns(new List<Bet> { 
                new Bet() { 
                    BetId = 1, User = new MundialitoUser() { Id = "1" } , Game = new Game() { GameId = 1, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 1, AwayScore = 1 , CardsMark = "X", CornersMark = "1"
                },
                new Bet() { 
                    BetId = 2, User = new MundialitoUser() { Id = "2" } , Game = new Game() { GameId = 1, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 2, AwayScore = 1 , CardsMark = "2", CornersMark = "1"
                },
                new Bet() { 
                    BetId = 3, User = new MundialitoUser() { Id = "3" } , Game = new Game() { GameId = 1, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 2, AwayScore = 2 , CardsMark = "1", CornersMark = "X"
                },
                new Bet() { 
                    BetId = 4, User = new MundialitoUser() { Id = "4" } , Game = new Game() { GameId = 2, Date = DateTime.Now.ToUniversalTime()}, HomeScore = 2, AwayScore = 2 , CardsMark = "X", CornersMark = "2"
                }
            });
            var resolver = CreateTarget(betsRepository.Object, new DateTimeProvider());
            resolver.ResolveBets(new Game()
            {
                GameId = 3,
                Date = DateTime.Now.ToUniversalTime(),
                HomeScore = 1,
                AwayScore = 1,
                CardsMark = "X",
                CornersMark = "1"
            });
            betsRepository.Setup(res => res.UpdateBet(It.Is<Bet>(bet => bet.BetId == 1))).Throws(new Exception("Should not be called"));
            betsRepository.Setup(res => res.UpdateBet(It.Is<Bet>(bet => bet.BetId == 2))).Throws(new Exception("Should not be called"));
            betsRepository.Setup(res => res.UpdateBet(It.Is<Bet>(bet => bet.BetId == 3))).Throws(new Exception("Should not be called"));
            betsRepository.Setup(res => res.UpdateBet(It.Is<Bet>(bet => bet.BetId == 4))).Throws(new Exception("Should not be called"));
            betsRepository.Setup(res => res.Save()).Throws(new Exception("Should not be called"));
        }

        private BetsResolver CreateTarget(IBetsRepository betsRepository, IDateTimeProvider dateTimeProvider)
        {
            var actionLogsRepository = new Mock<IActionLogsRepository>();
            return new BetsResolver(betsRepository, dateTimeProvider, actionLogsRepository.Object);
        }
    }
}

