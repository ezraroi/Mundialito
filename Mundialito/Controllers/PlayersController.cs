using Mundialito.DAL.Players;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Players")]
    [Authorize]
    public class PlayersController : ApiController
    {
        private readonly IPlayersRepository playersRepository;

        public PlayersController(IPlayersRepository playersRepository)
        {
            if (playersRepository == null)
                throw new ArgumentNullException("playersRepository");
            this.playersRepository = playersRepository;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return playersRepository.GetPlayers();
        }

    }
}
