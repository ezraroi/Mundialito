using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Web.Http;
using Mundialito.DAL.Stadiums;
using Mundialito.Models;
using Mundialito.DAL.Games;
using System.Diagnostics;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Stadiums")]
    [Authorize]
    public class StadiumsController : ApiController
    {
        private readonly IStadiumsRepository stadiumsRepository;
        private readonly IGamesRepository gamesRepository;

        public StadiumsController(IStadiumsRepository stadiumsRepository, IGamesRepository gamesRepository)
        {
            if (stadiumsRepository == null)
            {
                throw new ArgumentNullException("stadiumsRepository");
            }
            this.stadiumsRepository = stadiumsRepository;

            if (gamesRepository == null)
            {
                throw new ArgumentNullException("gamesRepository");
            }
            this.gamesRepository = gamesRepository;
        }

        public IEnumerable<Stadium>  GetAllStadiums()
        {
            return stadiumsRepository.GetStadiums();
        }

        public Stadium GetStadium(int id)
        {
            var item = stadiumsRepository.GetStadium(id);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("Stadium with id '{0}' not found", id));
            return item;
        }

        [Route("{id}/Games")]
        public IEnumerable<GameViewModel> GetStadiumGames(int id)
        {
            return gamesRepository.GetStadiumGames(id).Select(game => new GameViewModel(game));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Stadium PostStadium(Stadium stadium)
        {
            var res = stadiumsRepository.InsertStadium(stadium);
            stadiumsRepository.Save();
            return res;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public Stadium PutStadium(int id, Stadium stadium)
        {
            stadiumsRepository.UpdateStadium(stadium);
            stadiumsRepository.Save();
            return stadium;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void DeleteStadium(int id)
        {
            Trace.TraceInformation("Deleting Stadium {0}", id);
            stadiumsRepository.DeleteStadium(id);
            stadiumsRepository.Save();
        }

    }
}
