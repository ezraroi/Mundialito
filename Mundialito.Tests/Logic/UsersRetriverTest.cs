using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.Teams;
using Mundialito.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Tests.Logic
{
    [TestClass]
    public class UsersRetriverTest
    {
        private Team homeTeam = new Team() { TeamId = 1, Name = "Team1", ShortName = "TA1"};
        private Team awayTeam = new Team() { TeamId = 1, Name = "Team2", ShortName = "TA2"};

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void TestNonExistingUser()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository.Setup(item => item.GetUser(It.IsAny<String>())).Returns((MundialitoUser)null);
            var usersRetriver = new UsersRetriver(betsRepository.Object, usersRepository.Object);
            usersRetriver.GetUser("roi", false);
        }

        [TestMethod]
        public void TestGetNotLoggedUser()
        {
            var usersRepository = new Mock<IUsersRepository>();
            var user1 = CreateMundialtoUser("1");
            usersRepository.Setup(item => item.GetUser(It.IsAny<String>())).Returns(user1);

            var betsRepository = new Mock<IBetsRepository>();
            var openGame = CreateOpenGame(1);
            var clsoedGame = CreateClosedGame(2);
            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, openGame) { AwayScore = 1, HomeScore = 1 });
            allBets.Add(new Bet(user1, clsoedGame) { AwayScore = 1, HomeScore = 1, Points = 5 });
            betsRepository.Setup(item => item.GetUserBets(user1.UserName)).Returns(allBets);

            var usersRetriver = new UsersRetriver(betsRepository.Object, usersRepository.Object);
            var user = usersRetriver.GetUser("1", false);
            Assert.AreEqual(1, user.Bets.Count);
            Assert.IsFalse(user.Bets[0].IsOpenForBetting);
        }

        [TestMethod]
        public void TestGetLoggedUser()
        {
            var usersRepository = new Mock<IUsersRepository>();
            var user1 = CreateMundialtoUser("1");
            usersRepository.Setup(item => item.GetUser(It.IsAny<String>())).Returns(user1);

            var betsRepository = new Mock<IBetsRepository>();
            var openGame = CreateOpenGame(1);
            var clsoedGame = CreateClosedGame(2);
            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, openGame) { AwayScore = 1, HomeScore = 1 });
            allBets.Add(new Bet(user1, clsoedGame) { AwayScore = 1, HomeScore = 1, Points = 5 });
            betsRepository.Setup(item => item.GetUserBets(user1.UserName)).Returns(allBets);

            var usersRetriver = new UsersRetriver(betsRepository.Object, usersRepository.Object);
            var user = usersRetriver.GetUser("1", true);
            Assert.AreEqual(2, user.Bets.Count);
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            var betsRepository = new Mock<IBetsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            List<MundialitoUser> allUsers = new List<MundialitoUser>();
            allUsers.Add(new MundialitoUser() { Id = "1" });
            allUsers.Add(new MundialitoUser() { Id = "2" });
            usersRepository.Setup(item => item.AllUsers()).Returns(allUsers);
            var usersRetriver = new UsersRetriver(betsRepository.Object, usersRepository.Object);
            var res = usersRetriver.GetAllUsers();
            Assert.AreEqual(2, res.Count);
        }

        [TestMethod]
        public void TestGetAllUsersWithOpenBets()
        {
            var usersRepository = new Mock<IUsersRepository>();
            List<MundialitoUser> allUsers = new List<MundialitoUser>();
            var user1 = CreateMundialtoUser("1");
            var user2 = CreateMundialtoUser("2");
            allUsers.Add(user1);
            allUsers.Add(user2);
            usersRepository.Setup(item => item.AllUsers()).Returns(allUsers);

            var betsRepository = new Mock<IBetsRepository>();
            var openGame = CreateOpenGame(1);
            var clsoedGame = CreateClosedGame(2);
            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, openGame) { AwayScore = 1, HomeScore = 1 });
            allBets.Add(new Bet(user1, clsoedGame) { AwayScore = 1, HomeScore = 1, Points = 5 });
            allBets.Add(new Bet(user2, openGame) { AwayScore = 2, HomeScore = 0 });
            allBets.Add(new Bet(user2, clsoedGame) { AwayScore = 0, HomeScore = 1 });
            betsRepository.Setup(item => item.GetBets()).Returns(allBets);

            var usersRetriver = new UsersRetriver(betsRepository.Object, usersRepository.Object);
            var res = usersRetriver.GetAllUsers();
            Assert.AreEqual(2, res.Count);
            Assert.AreEqual(1, res[0].Bets.Count);
            Assert.AreEqual(1, res[1].Bets.Count);
            Assert.AreEqual(5, res[0].Points);
            Assert.AreEqual(0, res[1].Points);
        }

        private Game CreateOpenGame(int id)
        {
            return new Game() { GameId = id, Date = (DateTime.Now).Add(TimeSpan.FromDays(1)), HomeTeam = homeTeam , AwayTeam = awayTeam};
        }

        private Game CreateClosedGame(int id)
        {
            return new Game() { GameId = id, Date = (DateTime.Now).Subtract(TimeSpan.FromDays(1)), HomeScore = 1, AwayScore = 1, HomeTeam = homeTeam, AwayTeam = awayTeam };
        }

        private MundialitoUser CreateMundialtoUser(String id)
        {
            return new MundialitoUser() { Id = id, UserName = id };
        }
    }
}
