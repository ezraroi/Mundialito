using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mundialito.DAL;
using Mundialito.DAL.Games;
using Mundialito.DAL.Teams;
using Mundialito.Models;
using Mundialito.DAL.Bets;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;
using Mundialito.DAL.Accounts;
using Mundialito.Logic;
using Mundialito.DAL.ActionLogs;
using System.Web;
using System.Web.Configuration;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Games")]
    [Authorize]
    public class GamesController : ApiController
    {
        private const String ObjectType = "Game";
        private readonly IGamesRepository gamesRepository;
        private readonly IBetsRepository betsRepository;
        private readonly IBetsResolver betsResolver;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ILoggedUserProvider loggedUserProvider;
        private readonly IUsersRepository usersRepository;
        private readonly IActionLogsRepository actionLogsRepository;

        public GamesController(IGamesRepository gamesRepository, IBetsRepository betsRepository, IBetsResolver betsResolver, ILoggedUserProvider loggedUserProvider, IDateTimeProvider dateTimeProvider, IUsersRepository usersRepository, IActionLogsRepository actionLogsRepository)
        {
            if (gamesRepository == null)
                throw new ArgumentNullException("gamesRepository");
            this.gamesRepository = gamesRepository;

            if (betsRepository == null)
                throw new ArgumentNullException("betsRepository");
            this.betsRepository = betsRepository;

            if (betsResolver == null)
                throw new ArgumentNullException("betsResolver");
            this.betsResolver = betsResolver;

            if (loggedUserProvider == null)
                throw new ArgumentNullException("loggedUserProvider");
            this.loggedUserProvider = loggedUserProvider;

            if (dateTimeProvider == null)
                throw new ArgumentNullException("dateTimeProvider");
            this.dateTimeProvider = dateTimeProvider;

            if (dateTimeProvider == null)
                throw new ArgumentNullException("dateTimeProvider");
            this.dateTimeProvider = dateTimeProvider;

            if (usersRepository == null)
                throw new ArgumentNullException("usersRepository");
            this.usersRepository = usersRepository;

            if (actionLogsRepository == null)
                throw new ArgumentNullException("actionLogsRepository");
            this.actionLogsRepository = actionLogsRepository;
        }

        public IEnumerable<GameViewModel> GetAllGames()
        {
            var res = gamesRepository.GetGames().Select(game => new GameViewModel(game)).ToList();
            AddUserBetsData(res);
            return res;
        }

        [HttpGet]
        [Route("Stadium/{stadiumId}")]
        public IEnumerable<GameViewModel> GetStadiumGames(int stadiumId)
        {
            var res = gamesRepository.GetStadiumGames(stadiumId).Select(game => new GameViewModel(game)).ToList();
            AddUserBetsData(res);
            return res;
        }

        public GameViewModel GetGameByID(int id)
        {
            var item = gamesRepository.GetGame(id);
            if (item == null)
                throw new ObjectNotFoundException(string.Format("Game with id '{0}' not found", id));

            var res = new GameViewModel(item);
            res.UserHasBet = betsRepository.GetUserBetOnGame(loggedUserProvider.UserName, id) != null; 
            return res;
        }

        [Route("{id}/Bets")]
        public IEnumerable<BetViewModel> GetGameBets(int id)
        {
            var game = gamesRepository.GetGame(id);
            if (game == null)
                throw new ObjectNotFoundException(string.Format("Game with id '{0}' not found", id));

            if (game.IsOpen(dateTimeProvider.UTCNow))
                throw new ArgumentException(String.Format("Game '{0}' is stil open for betting", id));

            return betsRepository.GetGameBets(id).Select(item => new BetViewModel(item, dateTimeProvider.UTCNow)).OrderByDescending(bet => bet.Points);
        }

        [Route("{id}/MyBet/")]
        public BetViewModel GetGameUserBet(int id)
        {
            var game = GetGameByID(id);
            var uid = loggedUserProvider.UserId;
            var item =  betsRepository.GetGameBets(id).SingleOrDefault(bet => bet.User.Id == uid);
            if (item == null)
            {
                Trace.TraceInformation("No bet found for game {0} and user {1}, creating empty Bet", game.GameId, uid);
                return new BetViewModel() { BetId = -1, HomeScore = null, AwayScore = null,  IsOpenForBetting = true, IsResolved = false, Game = new BetGame() { GameId = id } };
            }
            return new BetViewModel(item, dateTimeProvider.UTCNow); 
        }

        [Route("Open")]
        public IEnumerable<GameViewModel> GetOpenGames()
        {
            var res = gamesRepository.GetGames().Where(game => game.IsOpen(dateTimeProvider.UTCNow)).Select(game => new GameViewModel(game));
            AddUserBetsData(res);
            return res;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public NewGameModel PostGame(NewGameModel game)
        {
            if (game.AwayTeam.TeamId == game.HomeTeam.TeamId)
                throw new ArgumentException("Home team and Away team can not be the same team");

            var newGame= new Game();
            newGame.HomeTeamId = game.HomeTeam.TeamId;
            newGame.AwayTeamId = game.AwayTeam.TeamId;
            newGame.StadiumId = game.Stadium.StadiumId;
            newGame.Date = game.Date;
            var res = gamesRepository.InsertGame(newGame);
            Trace.TraceInformation("Posting new Game: {0}", game);
            gamesRepository.Save();
            game.GameId = res.GameId;
            game.IsOpen = true;
            game.IsPendingUpdate = false;
            AddLog(ActionType.CREATE, String.Format("Posting new game: {0}", newGame));
            AddMonkeyBet(res);

            return game;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public PutGameModelResult PutGame(int id, PutGameModel game)
        {
            var item = gamesRepository.GetGame(id);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("No such game with id '{0}'", id));

            if (item.IsOpen(dateTimeProvider.UTCNow) && (game.HomeScore != null || game.AwayScore != null || game.CornersMark != null || game.CardsMark != null))
                throw new ArgumentException("Open game can not be updated with results");

            item.AwayScore = game.AwayScore;
            item.HomeScore = game.HomeScore;
            item.CardsMark = game.CardsMark;
            item.CornersMark = game.CornersMark;
            item.Date = game.Date;

            gamesRepository.UpdateGame(item);
            gamesRepository.Save();
            if (item.IsBetResolved(dateTimeProvider.UTCNow))
            {
                AddLog(ActionType.UPDATE, String.Format("Will resolve bets of game {0}", item.GameId));
                Trace.TraceInformation("Will reoslve Game {0} bets", id);
                betsResolver.ResolveBets(item);
            }
            AddLog(ActionType.UPDATE, String.Format("Updating Game {0}", item));
            return new PutGameModelResult(item, dateTimeProvider.UTCNow);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void DeleteGame(int id)
        {
            Trace.TraceInformation("Deleting Game {0}", id);
            gamesRepository.DeleteGame(id);
            gamesRepository.Save();
            AddLog(ActionType.DELETE, String.Format("Deleting Game {0}", id));
        }

        private void AddUserBetsData(IEnumerable<GameViewModel> res)
        {
            var allBets = betsRepository.GetUserBets(loggedUserProvider.UserName).ToDictionary(bet => bet.GameId, bet => bet);
            foreach (var game in res)
            {
                game.UserHasBet = allBets.ContainsKey(game.GameId);
            }
        }

        private void AddLog(ActionType actionType, String message)
        {
            try
            {
                actionLogsRepository.InsertLogAction(ActionLog.Create(actionType, ObjectType, message));
                actionLogsRepository.Save();
            }
            catch (Exception e)
            {
                Trace.TraceError("Exception during log. Exception: {0}", e.Message);
            }
        }

        private void AddMonkeyBet(Game res)
        {
            var monkeyUserName = WebConfigurationManager.AppSettings["MonkeyUserName"];
            if (!String.IsNullOrEmpty(monkeyUserName))
            {
                var monkeyUser = usersRepository.GetUser(monkeyUserName);
                if (monkeyUser == null)
                {
                    Trace.TraceError("Monkey user {0} was not found, will not add monkey bet", monkeyUserName);
                }
                var randomResults = new RandomResults();
                var result = randomResults.GetRandomResult();
                betsRepository.InsertBet(new Bet()
                {
                    
                    GameId = res.GameId,
                    UserId = monkeyUser.Id,
                    HomeScore = result.Key,
                    AwayScore = result.Value,
                    CardsMark = randomResults.GetRandomMark(),
                    CornersMark = randomResults.GetRandomMark()
                });
                betsRepository.Save();
            }
        }
    }
}

