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
using System.Data.Entity.Core;
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
        private List<Stadium> stadiums;

        [TestInitialize]
        public void CreateControllers()
        {
            AcceptenceTestsUtils.InitDatabase();
            users = AcceptenceTestsUtils.GetUsersController(new UserModel(String.Empty, "Admin"), DateTime.UtcNow).GetAllUsers().ToDictionary(item => item.Username, item => item);
            teams = AcceptenceTestsUtils.GetTeamsController().GetAllTeams().ToList();
            games = AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow).GetAllGames().ToList();
            stadiums = AcceptenceTestsUtils.GetStadiumsController().GetAllStadiums().ToList(); 
        }

        private UserModel GetUser(String username)
        {
            return users[username];
        }

        private Team GetTeam(int teamid)
        {
            return teams.SingleOrDefault(team => team.TeamId == teamid);
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

            TrySeeOtherUserGeneralBetBeforeTime();
            
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

            ValidateTableAfter2MoreGames();

            ValidateTableAfterAllGames();

            ValidateUerHasBet();

            TryResolveGeneralBetBeforeTime();

            ResolveUsersGeneralBets();
        }

        private void TrySeeOtherUserGeneralBetBeforeTime()
        {
            try
            {
                AcceptenceTestsUtils.GetGeneralBetsController(GetUser("User1"), TournamentTimesUtils.GeneralBetsCloseTime.Subtract(TimeSpan.FromDays(1))).GetUserGeneralBet("User2");
                throw new Exception("Operation should have failed");
            }
            catch (ArgumentException) { }
        }

        private void ValidateUerHasBet()
        {
            var games = AcceptenceTestsUtils.GetGamesController(GetUser("User1"), DateTime.UtcNow).GetAllGames();
            Assert.AreEqual(6, games.Count(game => game.UserHasBet));

            games = AcceptenceTestsUtils.GetGamesController(GetUser("User3"), DateTime.UtcNow).GetAllGames();
            Assert.AreEqual(5, games.Count(game => game.UserHasBet));
        }

        [TestMethod]
        public void AddAndEditGame()
        {
            var newGame = AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow).PostGame(new NewGameModel() { AwayTeam = teams[0], HomeTeam = teams[1], Date = DateTime.UtcNow.AddHours(2), Stadium = stadiums[0] });
            var updatedTime = DateTime.UtcNow.AddHours(3);
            var updatedGame = AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow).PutGame(newGame.GameId, new PutGameModel() { Date = updatedTime, HomeTeam = teams[1], AwayTeam = teams[0], Stadium = stadiums[1] });
            Assert.AreEqual(updatedTime, updatedGame.Date);
            Assert.AreEqual(stadiums[1].StadiumId, updatedGame.Stadium.StadiumId);

            try
            {
                updatedGame = AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow).PutGame(newGame.GameId, new PutGameModel() { HomeScore = 1, AwayScore = 1, CornersMark = "X", CardsMark = "X"});
                throw new Exception("Operation should have failed");
            }
            catch (ArgumentException) { }
        }

        private void ResolveUsersGeneralBets()
        {
            var generalBet = AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsResolveTime.AddDays(4)).GetUserGeneralBet("Admin");
            AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsResolveTime.AddDays(4)).ResolveGeneralBet(generalBet.GeneralBetId, new ResolveGeneralBetModel() { PlayerIsRight = true, TeamIsRight = false });

            generalBet = AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsResolveTime.AddDays(4)).GetUserGeneralBet("User1");
            AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsResolveTime.AddDays(4)).ResolveGeneralBet(generalBet.GeneralBetId, new ResolveGeneralBetModel() { PlayerIsRight = false, TeamIsRight = false });

            generalBet = AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsResolveTime.AddDays(4)).GetUserGeneralBet("User2");
            AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsResolveTime.AddDays(4)).ResolveGeneralBet(generalBet.GeneralBetId, new ResolveGeneralBetModel() { PlayerIsRight = false, TeamIsRight = true });

            try
            {
                AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsResolveTime.AddDays(4)).GetUserGeneralBet("User3");
                throw new Exception("Operation should have failed");
            }
            catch (ObjectNotFoundException) { }
            
            var allUsers = AcceptenceTestsUtils.GetUsersController(GetUser("User3"), DateTime.UtcNow.AddDays(4)).GetAllUsers().ToDictionary(item => item.Username, item => item);
            Assert.AreEqual(37, allUsers["Admin"].Points);
            Assert.AreEqual(16, allUsers["User1"].Points);
            Assert.AreEqual(35, allUsers["User2"].Points);
            Assert.AreEqual(7, allUsers["User3"].Points);
        }

        private void TryResolveGeneralBetBeforeTime()
        {
            try
            {
                var generalBet = AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(),DateTime.UtcNow.AddDays(2)).GetUserGeneralBet("Admin");
                AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsResolveTime.Subtract(TimeSpan.FromDays(2))).ResolveGeneralBet(generalBet.GeneralBetId, new ResolveGeneralBetModel() { PlayerIsRight = true, TeamIsRight = false });
                throw new Exception("Operation should have failed");
            }
            catch (ArgumentException) { }
        }

        private void ValidateTableAfterAllGames()
        {
            AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow.AddDays(3)).PutGame(games[4].GameId, CreateGameWithBetData(games[4], 1, 1, "1", "1"));
            AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow.AddDays(3)).PutGame(games[5].GameId, CreateGameWithBetData(games[5], 2, 0, "X", "1"));
            var allUsers = AcceptenceTestsUtils.GetUsersController(GetUser("User3"), DateTime.UtcNow.AddDays(3)).GetAllUsers().ToDictionary(item => item.Username, item => item);
            Assert.AreEqual(25, allUsers["Admin"].Points);
            Assert.AreEqual(16, allUsers["User1"].Points);
            Assert.AreEqual(23, allUsers["User2"].Points);
            Assert.AreEqual(7, allUsers["User3"].Points);

            Assert.AreEqual("1", allUsers["Admin"].Place);
            Assert.AreEqual("3", allUsers["User1"].Place);
            Assert.AreEqual("2", allUsers["User2"].Place);
            Assert.AreEqual("4", allUsers["User3"].Place);
        }

        private void ValidateTableAfter2MoreGames()
        {
            AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow.AddDays(2)).PutGame(games[2].GameId, CreateGameWithBetData(games[2], 2, 1, "2", "2"));
            AcceptenceTestsUtils.GetGamesController(GetAdmin(), DateTime.UtcNow.AddDays(2)).PutGame(games[3].GameId, CreateGameWithBetData(games[3], 0, 3, "1", "2"));
            var allUsers = AcceptenceTestsUtils.GetUsersController(GetUser("User2"), DateTime.UtcNow.AddDays(2)).GetAllUsers().ToDictionary(item => item.Username, item => item);
            Assert.AreEqual(19, allUsers["Admin"].Points);
            Assert.AreEqual(7, allUsers["User1"].Points);
            Assert.AreEqual(19, allUsers["User2"].Points);
            Assert.AreEqual(6, allUsers["User3"].Points);
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

        private PutGameModel CreateGameWithBetData(GameViewModel game, int homeScore, int awayScore, String cards, String corners)
        {
            var newGame = new PutGameModel();
            newGame.AwayScore = awayScore;
            newGame.AwayTeam = GetTeam(game.AwayTeam.TeamId);
            newGame.CardsMark = cards;
            newGame.CornersMark = corners;
            newGame.Date = game.Date;
            newGame.HomeScore = homeScore;
            newGame.HomeTeam = GetTeam(game.HomeTeam.TeamId);
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
                AcceptenceTestsUtils.GetGeneralBetsController(GetAdmin(), TournamentTimesUtils.GeneralBetsCloseTime.AddDays(1)).UpdateBet(generalBet.GeneralBetId, new UpdateGenralBetModel() { WinningTeamId = teams[3].TeamId, GoldenBootPlayer = "PlayerD" });
                throw new Exception("Operation should have failed");
            }
            catch (ArgumentException) { }
        }

        private void TryPostGeneralBetAfterTime()
        {
            
            try
            {
                AcceptenceTestsUtils.GetGeneralBetsController(GetUser("User3"), TournamentTimesUtils.GeneralBetsCloseTime.AddDays(1)).PostBet(new NewGeneralBetModel() { WinningTeamId = teams[1].TeamId, GoldenBootPlayer = "PlayerB" });
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
