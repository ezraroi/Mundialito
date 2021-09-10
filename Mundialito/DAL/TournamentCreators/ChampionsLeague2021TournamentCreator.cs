using Mundialito.DAL.Games;
using Mundialito.DAL.Players;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;

namespace Mundialito.DAL.DBCreators
{
    public class ChampionsLeague2021TournamentCreator : ITournamentCreator
    {
        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            return teams;
        }

        public List<Game> GetGames(Dictionary<String, Stadium> stadiums, Dictionary<String, Team> teams)
        {
            var games = new List<Game>();
            return games;
        }

        public List<Stadium> GetStadiums()
        {
            var stadiums = new List<Stadium>();
            return stadiums;
        }

        public List<Player> GetPlayers()
        {
            var players = new List<Player>();
            players.Add(new Player() { Name = "Erling Braut Haaland" });
            players.Add(new Player() { Name = "Lionel Messi" });
            players.Add(new Player() { Name = "Robert Lewandowski" });
            players.Add(new Player() { Name = "Romelu Lukaku" });
            players.Add(new Player() { Name = "Cristiano Ronaldo" });
            players.Add(new Player() { Name = "Karim Benzema" });
            players.Add(new Player() { Name = "Kylian Mbappe-Lottin" });
            players.Add(new Player() { Name = "Mohamed Salah" });
            players.Add(new Player() { Name = "Junior Neymar" });
            players.Add(new Player() { Name = "Miguel Bruno Fernandes" });
            players.Add(new Player() { Name = "Antoine Griezmann" });
            players.Add(new Player() { Name = "Memphis Depay" });
            players.Add(new Player() { Name = "Raheem Sterling" });
            players.Add(new Player() { Name = "Edinson Cavani" });
            players.Add(new Player() { Name = "Diogo Jota" });
            players.Add(new Player() { Name = "Fernando Gabriel Jesus" });
            players.Add(new Player() { Name = "Ferran Torres" });
            players.Add(new Player() { Name = "Jose Vinicius Junior" });
            players.Add(new Player() { Name = "Sadio Mane" });
            players.Add(new Player() { Name = "Luis Suarez" });
            players.Add(new Player() { Name = "Other" });
            return players;
        }

        private DateTime GetFixedDate(DateTime date)
        {
            return date.Subtract(TimeSpan.FromHours(3));
            //return date.AddDays(65);
        }

        private Team CreateTeam(String name, String shortName)
        {
            return new Team() { Name = name, ShortName = shortName, Flag = string.Format("https://api.fifa.com/api/v1/picture/flags-sq-2/{0}", shortName), Logo = string.Format("https://api.fifa.com/api/v1/picture/flags-sq-2/{0}", shortName) };
        }
    }
}
