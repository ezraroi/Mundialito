using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Web.Http;
using Mundialito.DAL.Teams;
using Mundialito.Models;
using System.Diagnostics;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Teams")]
    public class TeamsController : ApiController
    {
        private readonly ITeamsRepository teamsRepository;

        public TeamsController(ITeamsRepository teamsRepository )
        {
            if (teamsRepository == null)
            {
                throw new ArgumentNullException("teamsRepository"); 
            }
            this.teamsRepository = teamsRepository;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return teamsRepository.GetTeams();
        }

        public Team GetTeamById(int id)
        {
            var item = teamsRepository.GetTeam(id);

            if (item == null)
                throw new ObjectNotFoundException(string.Format("Team with id '{0}' not found", id));
            return item;
        }

        [Route("{id}/Games")]
        public IEnumerable<Game>  GetTeamGames(int id)
        {
            return teamsRepository.GetTeamGames(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Team PostTeam(Team team)
        {
            return teamsRepository.InsertTeam(team);
        }

    }
}
