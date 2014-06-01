using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Web.Http;
using Mundialito.DAL.Teams;
using Mundialito.Models;
using System.Diagnostics;
using System.Threading;
using Mundialito.DAL.Games;
using Mundialito.DAL.ActionLogs;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Teams")]
    [Authorize]
    public class TeamsController : ApiController
    {
        private readonly ITeamsRepository teamsRepository;
        private readonly IActionLogsRepository actionLogsRepository;

        public TeamsController(ITeamsRepository teamsRepository, IActionLogsRepository actionLogsRepository)
        {
            if (teamsRepository == null)
                throw new ArgumentNullException("teamsRepository"); 
            this.teamsRepository = teamsRepository;

            if (actionLogsRepository == null)
                throw new ArgumentNullException("actionLogsRepository");
            this.actionLogsRepository = actionLogsRepository;
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
        public IEnumerable<Game> GetTeamGames(int id)
        {
            return teamsRepository.GetTeamGames(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Team PostTeam(Team team)
        {
            var res = teamsRepository.InsertTeam(team);
            teamsRepository.Save();
            return res;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public Team PutTeam(int id,Team team)
        {
            teamsRepository.UpdateTeam(team);
            teamsRepository.Save();
            return team;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void DeleteTeam(int id)
        {
            Trace.TraceInformation("Deleting Team {0}", id);
            teamsRepository.DeleteTeam(id);
            teamsRepository.Save();
        }

    }
}
