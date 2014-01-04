using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Web.Http;
using Mundialito.DAL.Stadiums;
using Mundialito.Models;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Stadiums")]
    public class StadiumsController : ApiController
    {

        private readonly IStadiumsRepository stadiumsRepository;

        public StadiumsController(IStadiumsRepository stadiumsRepository)
        {
            if (stadiumsRepository == null)
            {
                throw new ArgumentNullException("stadiumsRepository");
            }
            this.stadiumsRepository = stadiumsRepository;
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
        public IEnumerable<Game> GetStadiumGames(int id)
        {
            return GetStadium(id).Games;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Stadium PostStadium(Stadium stadium)
        {
            return stadiumsRepository.InsertStadium(stadium);
        }

    }
}
