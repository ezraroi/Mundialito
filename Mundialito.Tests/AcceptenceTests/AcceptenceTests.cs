using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mundialito.Controllers;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.GeneralBets;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using Mundialito.Logic;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Tests.AcceptenceTests
{
    [TestClass]
    public class AcceptenceTests
    {
        private Dictionary<String, UserModel> users;
        private List<Team> teams;
        private List<Game> games;

        [TestInitialize]
        public void CreateControllers()
        {
            AcceptenceTestsUtils.InitDatabase();
            users = AcceptenceTestsUtils.GetUsersController(new UserModel(String.Empty, "Admin")).GetAllUsers().ToDictionary(item => item.Username, item => item);
            teams = AcceptenceTestsUtils.GetTeamsController().GetAllTeams().ToList();
            games = AcceptenceTestsUtils.GetGamesController().Get().ToList();
        }

        private UserModel GetUser(String username)
        {
            return users[username];
        }

        private UserModel GetAdmin()
        {
            return users["Admin"];
        }

        [TestMethod]
        public void MainAcceptenceTest()
        {
            TestUsersInitialState();

            PostGeneralBets();

            UpdateGeneralBet();

            TryPostGeneralBetAfterTime();

            TryUpdateGeneralBetAfterTime();

            PostAllUsersBets();

            TryPostBetOnClosedGame();

            TryUpdateBetOnClosedGame();

            TestOtherUsersCanNotSeeOpenGameBets();
        }

        private void TestOtherUsersCanNotSeeOpenGameBets()
        {
            var bets = AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow).GetUserBets("Admin");
            Assert.IsTrue(bets.Count() == 0);
        }

        private void TryUpdateBetOnClosedGame()
        {
            try
            {
                var bet = AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow).GetUserBets("User3").First();
                AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow.AddDays(4)).UpdateBet(bet.BetId, new UpdateBetModel() { AwayScore = 1, HomeScore = 2, CardsMark = "1", CornersMark = "2" });
                throw new Exception("Operation should have failed");
            }
            catch (ArgumentException) { }
        }

        private void PostGeneralBets()
        {
            AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), new DateTime(2014, 6, 1)).PostBet(new NewGeneralBetModel() { WinningTeamId = teams[0].TeamId, GoldenBootPlayer = "PlayerA" });
            AcceptenceTestsUtils.GetGeneralBetsController(GetUser("User1"), new DateTime(2014, 6, 1)).PostBet(new NewGeneralBetModel() { WinningTeamId = teams[1].TeamId, GoldenBootPlayer = "PlayerB" });
            AcceptenceTestsUtils.GetGeneralBetsController(GetUser("User2"), new DateTime(2014, 6, 1)).PostBet(new NewGeneralBetModel() { WinningTeamId = teams[2].TeamId, GoldenBootPlayer = "PlayerA" });
        }

        private void TryPostBetOnClosedGame()
        {
            try
            {
                AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow.AddDays(4)).PostBet(new NewBetModel() { GameId = games[5].GameId, AwayScore = 0, HomeScore = 2, CardsMark = "X", CornersMark = "1" });
                throw new Exception("Operation should have failed");
            }
            catch (ArgumentException) { }
        }

        private void PostAllUsersBets()
        {
            AcceptenceTestsUtils.GetBetsController(GetAdmin(), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[0].GameId, AwayScore = 0, HomeScore = 0, CardsMark = "X", CornersMark = "1" });
            AcceptenceTestsUtils.GetBetsController(GetAdmin(), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[1].GameId, AwayScore = 1, HomeScore = 0, CardsMark = "1", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetAdmin(), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[2].GameId, AwayScore = 1, HomeScore = 2, CardsMark = "2", CornersMark = "2" });
            AcceptenceTestsUtils.GetBetsController(GetAdmin(), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[3].GameId, AwayScore = 3, HomeScore = 1, CardsMark = "1", CornersMark = "2" });
            AcceptenceTestsUtils.GetBetsController(GetAdmin(), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[4].GameId, AwayScore = 2, HomeScore = 2, CardsMark = "2", CornersMark = "1" });
            AcceptenceTestsUtils.GetBetsController(GetAdmin(), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[5].GameId, AwayScore = 1, HomeScore = 1, CardsMark = "X", CornersMark = "1" });

            AcceptenceTestsUtils.GetBetsController(GetUser("User1"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[0].GameId, AwayScore = 2, HomeScore = 1, CardsMark = "1", CornersMark = "2" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User1"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[1].GameId, AwayScore = 0, HomeScore = 3, CardsMark = "X", CornersMark = "1" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User1"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[2].GameId, AwayScore = 2, HomeScore = 1, CardsMark = "1", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User1"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[3].GameId, AwayScore = 2, HomeScore = 2, CardsMark = "1", CornersMark = "1" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User1"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[4].GameId, AwayScore = 1, HomeScore = 1, CardsMark = "1", CornersMark = "2" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User1"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[5].GameId, AwayScore = 0, HomeScore = 1, CardsMark = "2", CornersMark = "X" });

            AcceptenceTestsUtils.GetBetsController(GetUser("User2"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[0].GameId, AwayScore = 2, HomeScore = 1, CardsMark = "2", CornersMark = "1" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User2"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[1].GameId, AwayScore = 2, HomeScore = 1, CardsMark = "2", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User2"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[2].GameId, AwayScore = 1, HomeScore = 4, CardsMark = "2", CornersMark = "2" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User2"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[3].GameId, AwayScore = 3, HomeScore = 1, CardsMark = "1", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User2"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[4].GameId, AwayScore = 2, HomeScore = 2, CardsMark = "1", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User2"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[5].GameId, AwayScore = 1, HomeScore = 1, CardsMark = "1", CornersMark = "X" });

            AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[0].GameId, AwayScore = 2, HomeScore = 3, CardsMark = "X", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[1].GameId, AwayScore = 3, HomeScore = 0, CardsMark = "2", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[2].GameId, AwayScore = 0, HomeScore = 0, CardsMark = "X", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[3].GameId, AwayScore = 0, HomeScore = 2, CardsMark = "1", CornersMark = "X" });
            AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow).PostBet(new NewBetModel() { GameId = games[4].GameId, AwayScore = 0, HomeScore = 2, CardsMark = "X", CornersMark = "1" });
        }

        private void TryUpdateGeneralBetAfterTime()
        {
            try
            {
                var generalBet = AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), new DateTime(2014, 6, 2)).GetUserGeneralBet("Admin");
                AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), new DateTime(2014, 6, 13)).UpdateBet(generalBet.GeneralBetId, new UpdateGenralBetModel() { WinningTeamId = teams[3].TeamId, GoldenBootPlayer = "PlayerD" });
                throw new Exception("Operation should have failed");
            }
            catch (ArgumentException) { }
        }

        private void TryPostGeneralBetAfterTime()
        {
            try
            {
                AcceptenceTestsUtils.GetGeneralBetsController(GetUser("User3"), new DateTime(2014, 6, 13)).PostBet(new NewGeneralBetModel() { WinningTeamId = teams[1].TeamId, GoldenBootPlayer = "PlayerB" });
                throw new Exception("Operation should have failed");
            }
            catch (ArgumentException) { }
        }

        private void UpdateGeneralBet()
        {
            var generalBet = AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), new DateTime(2014, 6, 2)).GetUserGeneralBet("Admin");
            AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), new DateTime(2014, 6, 2)).UpdateBet(generalBet.GeneralBetId, new UpdateGenralBetModel() { WinningTeamId = teams[3].TeamId, GoldenBootPlayer = "PlayerD" });
            var updatedGeneralBet = AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), new DateTime(2014, 6, 2)).GetUserGeneralBet("Admin");
            Assert.AreEqual(generalBet.GeneralBetId, updatedGeneralBet.GeneralBetId);
            Assert.AreEqual(GetAdmin().Name, updatedGeneralBet.OwnerName);
            Assert.AreEqual(teams[3].TeamId, updatedGeneralBet.WinningTeamId);
            Assert.AreEqual("PlayerD", updatedGeneralBet.GoldenBootPlayer);
        }

        private void TestUsersInitialState()
        {
            Assert.AreEqual(4, users.Count());
            foreach (var user in users.Values)
            {
                Assert.AreEqual(0, user.Points);
                Assert.AreEqual(0, user.Results + user.Marks + user.Corners + user.YellowCards);
            }
        }
    }
}
