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

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Games")]
    [Authorize]
    public class GamesController : ApiController
    {
        private readonly IGamesRepository gamesRepository;
        private readonly IBetsRepository betsRepository;
        private readonly UserManager<MundialitoUser> usersManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(new MundialitoContext()));

        public GamesController(IGamesRepository gamesRepository, IBetsRepository betsRepository)
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
        }

        public IEnumerable<IGame> Get()
        {
            return gamesRepository.GetGames();
        }

        public IGame GetGameByID(int id)
        {
            var item = gamesRepository.GetGame(id);
            if (item == null)
                throw new ObjectNotFoundException(string.Format("Game with id '{0}' not found", id));
            return item; 
        }

        [Route("{id}/Bets")]
        public IEnumerable<BetViewModel> GetGameBets(int id)
        {
            return betsRepository.GetGameBets(id).Select(item => new BetViewModel(item));
        }

        [Route("{id}/MyBet/")]
        public BetViewModel GetGameUserBet(int id)
        {
            var game = GetGameByID(id);
            var uid = User.Identity.GetUserId();
            var item =  betsRepository.GetGameBets(id).SingleOrDefault(bet => bet.User.Id == uid);
            if (item == null)
            {
                Trace.TraceInformation("No bet found for game {0} and user {1}, creating empty Bet", game.GameId, uid);
                return new BetViewModel() { BetId = -1, Game = new BetGame() { GameId = id } };
            }
            return new BetViewModel(item); 
        }

        [Route("Open")]
        public IEnumerable<IGame> GetOpenGames()
        {
            return
                gamesRepository.GetGames().Where(game => game.IsOpen);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IGame PostGame(IGame game)
        {
            if (game.AwayTeam.TeamId == game.HomeTeam.TeamId)
                throw new ArgumentException("Home team and Away team can not be the same team");
            var res = gamesRepository.InsertGame(game);
            gamesRepository.Save();
            return res;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public Game PutGame(int id, Game game)
        {
            gamesRepository.UpdateGame(game);
            gamesRepository.Save();
            return game;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void DeleteGame(int id)
        {
            gamesRepository.DeleteGame(id);
            gamesRepository.Save();
        }

    }
}

