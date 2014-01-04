using System;
using System.Collections.Generic;
using Mundialito.Models;

namespace Mundialito.DAL.Teams
{
    public interface ITeamsRepository : IDisposable
    {
        IEnumerable<Team> GetTeams();
        IEnumerable<Game> GetTeamGames(int teamId);
        Team GetTeam(int teamId);
        Team InsertTeam(Team team);
        void DeleteTeam(int teamId);
        void UpdateTeam(Team team);
        void Save(); 

    }
}