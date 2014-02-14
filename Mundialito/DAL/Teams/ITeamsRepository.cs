using System;
using System.Collections.Generic;
using Mundialito.Models;
using Mundialito.DAL.Games;

namespace Mundialito.DAL.Teams
{
    public interface ITeamsRepository : IDisposable
    {
        IEnumerable<Team> GetTeams();
        IEnumerable<IGame> GetTeamGames(int teamId);
        Team GetTeam(int teamId);
        Team InsertTeam(Team team);
        void DeleteTeam(int teamId);
        void UpdateTeam(Team team);
        void Save(); 

    }
}