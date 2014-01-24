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

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Games")]
    public class GamesController : ApiController
    {
        private readonly IGamesRepository gamesRepository;
        private readonly IBetsRepository betsRepository;

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

        public IEnumerable<Game> Get()
        {
            return gamesRepository.GetGames();
        }

        public Game GetGameByID(int id)
        {
            var item = gamesRepository.GetGame(id);
            if (item == null)
                throw new ObjectNotFoundException(string.Format("Game with id '{0}' not found", id));
            return item; 
        }

        [Route("{id}/Bets")]
        public IEnumerable<Bet> GetGameBets(int id)
        {
            return betsRepository.GetGameBets(id);
        }

        [Route("Open")]
        public IEnumerable<Game> GetOpenGames()
        {
            return
                gamesRepository.GetGames().Where(game => game.IsOpen);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Game PostGame(Game game)
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

