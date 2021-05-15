using Mundialito.DAL.Games;
using Mundialito.DAL.Players;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;

namespace Mundialito.DAL.DBCreators
{
    public interface ITournamentCreator
    {
        List<Team> GetTeams();
        List<Game> GetGames(Dictionary<String, Stadium> stadiums, Dictionary<String, Team> teams);
        List<Stadium> GetStadiums();
        List<Player> GetPlayers();
    }
}
