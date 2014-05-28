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

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Games")]
    [Authorize]
    public class GamesController : ApiController
    {
        private readonly IGamesRepository gamesRepository;
        private readonly IBetsRepository betsRepository;
        private readonly IBetsResolver betsResolver;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ILoggedUserProvider loggedUserProvider;
        
        public GamesController(IGamesRepository gamesRepository, IBetsRepository betsRepository, IBetsResolver betsResolver, ILoggedUserProvider loggedUserProvider,  IDateTimeProvider dateTimeProvider)
        {
            if (gamesRepository == null)
            {
                throw new ArgumentNullException("gamesRepository");
            }
            this.gamesRepository = gamesRepository;

            if (betsRepository == null)
            {
                throw new ArgumentNullException("betsRepository");
            }
            this.betsRepository = betsRepository;

            if (betsResolver == null)
            {
                throw new ArgumentNullException("betsResolver");
            }
            this.betsResolver = betsResolver;


            if (loggedUserProvider == null)
            {
                throw new ArgumentNullException("loggedUserProvider");
            }
            this.loggedUserProvider = loggedUserProvider;

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("dateTimeProvider");
            }
            this.dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<GameViewModel> GetAllGames()
        {
            return gamesRepository.GetGames().Select(game => new GameViewModel(game));
        }

        public GameViewModel GetGameByID(int id)
        {
            var item = gamesRepository.GetGame(id);
            if (item == null)
                throw new ObjectNotFoundException(string.Format("Game with id '{0}' not found", id));
            return new GameViewModel(item); 
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
        public IEnumerable<Game> GetOpenGames()
        {
            return gamesRepository.GetGames().Where(game => game.IsOpen(dateTimeProvider.UTCNow));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Game PostGame(Game game)
        {
            if (game.AwayTeam.TeamId == game.HomeTeam.TeamId)
                throw new ArgumentException("Home team and Away team can not be the same team");

            var res = gamesRepository.InsertGame(game);
            Trace.TraceInformation("Posting new Game: {0}", game);
            gamesRepository.Save();
            return res;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public Game PutGame(int id, Game game)
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
            item.HomeTeam = game.HomeTeam;
            item.Stadium = game.Stadium;
            item.AwayTeam = game.AwayTeam;

            gamesRepository.UpdateGame(item);
            gamesRepository.Save();
            if (item.IsBetResolved(dateTimeProvider.UTCNow))
            {
                Trace.TraceInformation("Will reoslve Game {0} bets", game.GameId);
                betsResolver.ResolveBets(item);
            }
            return game;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void DeleteGame(int id)
        {
            Trace.TraceInformation("Deleting Game {0}", id);
            gamesRepository.DeleteGame(id);
            gamesRepository.Save();
        }
    }
}

