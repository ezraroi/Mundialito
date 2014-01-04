using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Mundialito.Models;

namespace Mundialito.DAL.Teams
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly MundialitoContext context = new MundialitoContext();
        private bool disposed = false;

        #region Implementation of ITeamsRepository

        public IEnumerable<Team> GetTeams()
        {
            return context.Teams;
        }

        public IEnumerable<Game> GetTeamGames(int teamId)
        {
            return context.Games.Where(game => game.HomeTeam.TeamId == teamId || game.AwayTeam.TeamId == teamId).Include(game => game.AwayTeam).Include(game => game.HomeTeam);
        }

        public Team GetTeam(int teamId)
        {
            return context.Teams.Find(teamId);
        }

        public Team InsertTeam(Team team)
        {
            return context.Teams.Add(team);
        }

        public void DeleteTeam(int teamId)
        {
            var team = GetTeam(teamId);
            context.Teams.Remove(team);
        }

        public void UpdateTeam(Team team)
        {
            context.Entry(team).State = EntityState.Modified; 
        }

        public void Save()
        {
            context.SaveChanges(); 
        }

        #endregion

        #region Implementation of IDisposable
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
    }
}