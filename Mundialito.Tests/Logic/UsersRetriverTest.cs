using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.GeneralBets;
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
            var generalBetsRepository = new Mock<IGeneralBetsRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository.Setup(item => item.GetUser(It.IsAny<String>())).Returns((MundialitoUser)null);
            var usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, new DateTimeProvider());
            usersRetriver.GetUser("roi", false);
        }

        [TestMethod]
        public void TestGetNotLoggedUser()
        {
            var generalBetsRepository = new Mock<IGeneralBetsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            var user1 = CreateMundialtoUser("1");
            usersRepository.Setup(item => item.GetUser(It.IsAny<String>())).Returns(user1);

            var betsRepository = new Mock<IBetsRepository>();
            var openGame = CreateOpenGame(1);
            var clsoedGame = CreateClosedGame(2);
            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, openGame) { AwayScore = 1, HomeScore = 1 });
            allBets.Add(new Bet(user1, clsoedGame) { AwayScore = 1, HomeScore = 1, Points = 5, ResultWin = true });
            betsRepository.Setup(item => item.GetUserBets(user1.UserName)).Returns(allBets);

            var usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, new DateTimeProvider());
            var user = usersRetriver.GetUser("1", false);
            Assert.AreEqual(5, user.Points);
            Assert.AreEqual(1, user.Results);
            Assert.AreEqual(0, user.Corners + user.YellowCards + user.Marks);
        }

        [TestMethod]
        public void TestGetNotLoggedUser2()
        {
            var generalBetsRepository = new Mock<IGeneralBetsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            var user1 = CreateMundialtoUser("1");
            usersRepository.Setup(item => item.GetUser(It.IsAny<String>())).Returns(user1);

            var betsRepository = new Mock<IBetsRepository>();
            var openGame1 = CreateOpenGame(1);
            var openGame2 = CreateOpenGame(2);
            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, openGame1) { AwayScore = 1, HomeScore = 1 });
            allBets.Add(new Bet(user1, openGame2) { AwayScore = 1, HomeScore = 1 });
            betsRepository.Setup(item => item.GetUserBets(user1.UserName)).Returns(allBets);

            var usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, new DateTimeProvider());
            var user = usersRetriver.GetUser("1", false);
            Assert.AreEqual(0, user.Points);
            Assert.AreEqual(0, user.Corners + user.YellowCards + user.Marks + user.Results);
        }

        [TestMethod]
        public void TestGetLoggedUser()
        {
            var generalBetsRepository = new Mock<IGeneralBetsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            var user1 = CreateMundialtoUser("1");
            usersRepository.Setup(item => item.GetUser(It.IsAny<String>())).Returns(user1);

            var betsRepository = new Mock<IBetsRepository>();
            var openGame = CreateOpenGame(1);
            var clsoedGame = CreateClosedGame(2);
            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, openGame) { AwayScore = 1, HomeScore = 1 });
            allBets.Add(new Bet(user1, clsoedGame) { AwayScore = 1, HomeScore = 1, Points = 5 , ResultWin = true });
            betsRepository.Setup(item => item.GetUserBets(user1.UserName)).Returns(allBets);

            var usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, new DateTimeProvider());
            var user = usersRetriver.GetUser("1", true);
            Assert.AreEqual(5, user.Points);
            Assert.AreEqual(1, user.Results);
            Assert.AreEqual(0, user.Corners + user.YellowCards + user.Marks);
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            var generalBetsRepository = new Mock<IGeneralBetsRepository>();
            var betsRepository = new Mock<IBetsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            List<MundialitoUser> allUsers = new List<MundialitoUser>();
            allUsers.Add(new MundialitoUser() { Id = "1" });
            allUsers.Add(new MundialitoUser() { Id = "2" });
            usersRepository.Setup(item => item.AllUsers()).Returns(allUsers);
            var usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, new DateTimeProvider());
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
            var user3 = CreateMundialtoUser("3");
            allUsers.Add(user1);
            allUsers.Add(user2);
            allUsers.Add(user3);
            usersRepository.Setup(item => item.AllUsers()).Returns(allUsers);

            var betsRepository = new Mock<IBetsRepository>();
            var openGame = CreateOpenGame(1);
            var clsoedGame = CreateClosedGame(2);
            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, openGame) { AwayScore = 1, HomeScore = 1 });
            allBets.Add(new Bet(user1, clsoedGame) { AwayScore = 1, HomeScore = 1, Points = 5, ResultWin = true });
            allBets.Add(new Bet(user2, openGame) { AwayScore = 2, HomeScore = 0 });
            allBets.Add(new Bet(user2, clsoedGame) { AwayScore = 0, HomeScore = 1 });
            allBets.Add(new Bet(user3, openGame) { AwayScore = 2, HomeScore = 0 });
            allBets.Add(new Bet(user3, clsoedGame) { AwayScore = 0, HomeScore = 1 });
            betsRepository.Setup(item => item.GetBets()).Returns(allBets);

            var generalBetsRepository = new Mock<IGeneralBetsRepository>();
            List<GeneralBet> generalBets = new List<GeneralBet>();
            generalBets.Add(new GeneralBet() { User = user1, IsResolved = true, PlayerPoints = 12, TeamPoints = 0, WinningTeamId = 1 });
            generalBets.Add(new GeneralBet() { User = user2, IsResolved = false, WinningTeamId = 1 });
            generalBets.Add(new GeneralBet() { User = user3, IsResolved = true, PlayerPoints = 12, TeamPoints = 12, WinningTeamId = 2 });
            generalBetsRepository.Setup(rep => rep.GetGeneralBets()).Returns(generalBets);

            var usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, new DateTimeProvider());
            var res = usersRetriver.GetAllUsers();
            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(17, res[0].Points);
            Assert.AreEqual(1, res[0].Results);
            Assert.AreEqual(0, res[1].Points);
            Assert.AreEqual(24, res[2].Points);
        }

        [TestMethod]
        public void TestGetAllUsersYesterdayPoints()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var usersRepository = new Mock<IUsersRepository>();
            var generalBetsRepository = new Mock<IGeneralBetsRepository>();
            List<MundialitoUser> allUsers = new List<MundialitoUser>();
            var user1 = CreateMundialtoUser("1");
            var user2 = CreateMundialtoUser("2");
            var user3 = CreateMundialtoUser("3");
            allUsers.Add(user1);
            allUsers.Add(user2);
            allUsers.Add(user3);
            usersRepository.Setup(item => item.AllUsers()).Returns(allUsers);

            var betsRepository = new Mock<IBetsRepository>();
            var clsoedGame1 = new Game() { GameId = 1, Date = (DateTime.Now).Subtract(TimeSpan.FromDays(1)), HomeTeam = homeTeam, AwayTeam = awayTeam };
            var clsoedGame2 = new Game() { GameId = 2, Date = (DateTime.Now).Subtract(TimeSpan.FromDays(2)), HomeTeam = homeTeam, AwayTeam = awayTeam };

            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, clsoedGame1) { AwayScore = 1, HomeScore = 1, Points = 5 });
            allBets.Add(new Bet(user1, clsoedGame2) { AwayScore = 1, HomeScore = 1, Points = 1 });
            allBets.Add(new Bet(user2, clsoedGame1) { AwayScore = 2, HomeScore = 0, Points = 1 });
            allBets.Add(new Bet(user2, clsoedGame2) { AwayScore = 0, HomeScore = 1, Points = 7 });
            allBets.Add(new Bet(user3, clsoedGame1) { AwayScore = 2, HomeScore = 0, Points = 2 });
            allBets.Add(new Bet(user3, clsoedGame2) { AwayScore = 0, HomeScore = 1, Points = 10 });
            betsRepository.Setup(item => item.GetBets()).Returns(allBets);
            
            dateTimeProvider.Setup(item => item.UTCNow).Returns(clsoedGame2.Date);
            var usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, dateTimeProvider.Object);
            var res = usersRetriver.GetAllUsers();
            Assert.AreEqual(0, res.Sum(user => user.YesterdayPoints));

            dateTimeProvider = new Mock<IDateTimeProvider>();
            usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, new DateTimeProvider());
            res = usersRetriver.GetAllUsers();
            Assert.AreEqual(18, res.Sum(user => user.YesterdayPoints));

            dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(item => item.UTCNow).Returns(DateTime.Now.AddDays(1));
            usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, dateTimeProvider.Object);
            res = usersRetriver.GetAllUsers();
            Assert.AreEqual(26, res.Sum(user => user.YesterdayPoints));
        }

        [TestMethod]
        public void TestUserTotalMarks()
        {
            var generalBetsRepository = new Mock<IGeneralBetsRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            var user1 = CreateMundialtoUser("1");
            usersRepository.Setup(item => item.GetUser(It.IsAny<String>())).Returns(user1);

            var betsRepository = new Mock<IBetsRepository>();
            var closedGame1 = CreateClosedGame(1);
            var closedGame2 = CreateClosedGame(2);
            var closedGame3 = CreateClosedGame(3);
            List<Bet> allBets = new List<Bet>();
            allBets.Add(new Bet(user1, closedGame1) { AwayScore = 1, HomeScore = 1, Points = 2, CardsWin = true, CornersWin = true });
            allBets.Add(new Bet(user1, closedGame2) { AwayScore = 1, HomeScore = 1, Points = 5, GameMarkWin = true, CornersWin = true, CardsWin = true });
            allBets.Add(new Bet(user1, closedGame3) { AwayScore = 1, HomeScore = 1, Points = 6, ResultWin = true , GameMarkWin = true, CardsWin = true});
            betsRepository.Setup(item => item.GetUserBets(user1.UserName)).Returns(allBets);

            var usersRetriver = new UsersRetriver(betsRepository.Object, generalBetsRepository.Object, usersRepository.Object, new DateTimeProvider());
            var user = usersRetriver.GetUser("1", false);
            Assert.AreEqual(13, user.Points);
            Assert.AreEqual(1, user.Results);
            Assert.AreEqual(3, user.YellowCards);
            Assert.AreEqual(1, user.Marks);
            Assert.AreEqual(2, user.Corners);

            user = usersRetriver.GetUser("1", true);
            Assert.AreEqual(13, user.Points);
            Assert.AreEqual(1, user.Results);
            Assert.AreEqual(3, user.YellowCards);
            Assert.AreEqual(1, user.Marks);
            Assert.AreEqual(2, user.Corners);
        }

        private Game CreateOpenGame(int id)
        {
            return new Game() { GameId = id, Date = (DateTime.Now).Add(TimeSpan.FromDays(1)), HomeTeam = homeTeam , AwayTeam = awayTeam};
        }

        private Game CreateClosedGame(int id)
        {
            return new Game() { GameId = id, Date = (DateTime.Now).Subtract(TimeSpan.FromDays(1)), HomeScore = 1, AwayScore = 1, CornersMark = "X", CardsMark = "2",  HomeTeam = homeTeam, AwayTeam = awayTeam };
        }

        private MundialitoUser CreateMundialtoUser(String id)
        {
            return new MundialitoUser() { Id = id, UserName = id };
        }
    }
}