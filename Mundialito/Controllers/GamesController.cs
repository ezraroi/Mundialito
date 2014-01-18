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

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Games")]
    public class GamesController : ApiController
    {
        private readonly IGamesRepository gamesRepository;

        public GamesController(IGamesRepository gamesRepository, ITeamsRepository teamsRepository)
        {
            if (gamesRepository == null)
            {
                throw new ArgumentNullException("gamesRepository");
            }
            this.gamesRepository = gamesRepository;
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

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void DeleteGame(int id)
        {
            gamesRepository.DeleteGame(id);
            gamesRepository.Save();
        }

    }
}
