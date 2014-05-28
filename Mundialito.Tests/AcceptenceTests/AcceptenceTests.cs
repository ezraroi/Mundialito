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
        private List<GameViewModel> games;

        [TestInitialize]
        public void CreateControllers()
        {
            AcceptenceTestsUtils.InitDatabase();
            users = AcceptenceTestsUtils.GetUsersController(new UserModel(String.Empty, "Admin"), DateTime.UtcNow).GetAllUsers().ToDictionary(item => item.Username, item => item);
            teams = AcceptenceTestsUtils.GetTeamsController().GetAllTeams().ToList();
            games = AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow).GetAllGames().ToList();
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

            TryUpdateOtherUserBet();

            GetAllLoggedUserBets();

            GetOpenGames();

            ValidateUserBetOnGame();

            ValidateTableAfter2Games();

            ValidateTableAfterGameResultUpdate();

            // TODO - Test adding a new game and updating his date
        }

        private void ValidateTableAfterGameResultUpdate()
        {
            AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow.AddDays(1)).PutGame(games[1].GameId, CreateGameWithBetData(games[1], 0, 1, "2", "1"));
            var allUsers = AcceptenceTestsUtils.GetUsersController(GetUser("User1"), DateTime.UtcNow.AddDays(1)).GetAllUsers().ToDictionary(item => item.Username, item => item);
            Assert.AreEqual(7, allUsers["Admin"].Points);
            Assert.AreEqual(6, allUsers["User1"].Points);
            Assert.AreEqual(10, allUsers["User2"].Points);
            Assert.AreEqual(5, allUsers["User3"].Points);
        }

        private void ValidateTableAfter2Games()
        {
            AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow.AddDays(1)).PutGame(games[0].GameId, CreateGameWithBetData(games[0], 1, 2, "X", "1"));
            AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow.AddDays(1)).PutGame(games[1].GameId, CreateGameWithBetData(games[1], 1, 1, "2", "X"));
            var allUsers = AcceptenceTestsUtils.GetUsersController(GetUser("User1"), DateTime.UtcNow.AddDays(1)).GetAllUsers().ToDictionary(item => item.Username, item => item);
            Assert.AreEqual(3, allUsers["Admin"].Points);
            Assert.AreEqual(5, allUsers["User1"].Points);
            Assert.AreEqual(8, allUsers["User2"].Points);
            Assert.AreEqual(3, allUsers["User3"].Points);
        }

        private Game CreateGameWithBetData(GameViewModel game, int homeScore, int awayScore, String cards, String corners)
        {
            var newGame = new Game();
            newGame.GameId = game.GameId;
            newGame.AwayScore = awayScore;
            newGame.AwayTeam = game.AwayTeam;
            newGame.CardsMark = cards;
            newGame.CornersMark = corners;
            newGame.Date = game.Date;
            newGame.HomeScore = homeScore;
            newGame.HomeTeam = game.HomeTeam;
            newGame.Stadium = game.Stadium;
            return newGame;
        }

        private void ValidateUserBetOnGame()
        {
            var userBet = AcceptenceTestsUtils.GetGamesController(GetUser("User1"), DateTime.Now).GetGameUserBet(games[0].GameId);
            Assert.AreEqual(2, userBet.AwayScore);
            Assert.AreEqual(1, userBet.HomeScore);
            Assert.AreEqual("1", userBet.CardsMark);
            Assert.AreEqual("2", userBet.CornersMark);
        }

        private void GetOpenGames()
        {
            Assert.AreEqual(6, AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow).GetOpenGames().Count());
            Assert.AreEqual(4, AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow.AddDays(1)).GetOpenGames().Count());
        }

        private void GetAllLoggedUserBets()
        {
            var bets = AcceptenceTestsUtils.GetBetsController(GetUser("User1"), DateTime.UtcNow).GetUserBets("User1");
            Assert.AreEqual(6, bets.Count());
        }

        private void TryUpdateOtherUserBet()
        {
            try
            {
                var bet = AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow.AddDays(2)).GetUserBets("Admin").First();
                AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow.AddDays(2)).UpdateBet(bet.BetId, new UpdateBetModel() { AwayScore = 1, HomeScore = 2, CardsMark = "1", CornersMark = "2" });
                throw new Exception("Operation should have failed");
            }
            catch (UnauthorizedAccessException) { }
        }

        private void TestOtherUsersCanNotSeeOpenGameBets()
        {
            var bets = AcceptenceTestsUtils.GetBetsController(GetUser("User3"), DateTime.UtcNow.AddDays(2)).GetUserBets("Admin");
            Assert.IsTrue(bets.Count() == 4);
            bets.ToList().ForEach(bet => Assert.IsFalse(bet.IsOpenForBetting));
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
