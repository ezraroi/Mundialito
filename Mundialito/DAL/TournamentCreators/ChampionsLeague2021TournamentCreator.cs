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
            teams.Add(CreateTeam("AFC Ajax", "NED"));
            teams.Add(CreateTeam("Atalanta BC", "ITA"));
            teams.Add(CreateTeam("Club Atlético de Madrid", "ESP"));
            teams.Add(CreateTeam("FC Barcelona", "ESP"));
            teams.Add(CreateTeam("FC Bayern München", "GER"));
            teams.Add(CreateTeam("SL Benfica", "POR"));
            teams.Add(CreateTeam("Beşiktaş JK", "TUR"));
            teams.Add(CreateTeam("Chelsea FC", "ENG"));
            teams.Add(CreateTeam("Club Brugge", "BEL"));
            teams.Add(CreateTeam("Borussia Dortmund", "GER"));
            teams.Add(CreateTeam("FC Dynamo Kyiv", "UKR"));
            teams.Add(CreateTeam("FC Internazionale Milano", "ITA"));
            teams.Add(CreateTeam("Juventus", "ITA"));
            teams.Add(CreateTeam("RB Leipzig", "GER"));
            teams.Add(CreateTeam("Liverpool FC", "ENG"));
            teams.Add(CreateTeam("LOSC Lille", "FRA"));
            teams.Add(CreateTeam("Malmö FF", "SWE"));
            teams.Add(CreateTeam("Manchester City FC", "ENG"));
            teams.Add(CreateTeam("Manchester United", "ENG"));
            teams.Add(CreateTeam("AC Milan", "ITA"));
            teams.Add(CreateTeam("Paris Saint-Germain", "FRA"));
            teams.Add(CreateTeam("FC Porto", "POR"));
            teams.Add(CreateTeam("Real Madrid CF", "ESP"));
            teams.Add(CreateTeam("FC Salzburg", "AUT"));
            teams.Add(CreateTeam("Sevilla FC", "ESP"));
            teams.Add(CreateTeam("FC Shakhtar Donetsk", "UKR"));
            teams.Add(CreateTeam("FC Sheriff Tiraspol", "MDA"));
            teams.Add(CreateTeam("Sporting Clube de Portugal", "POR"));
            teams.Add(CreateTeam("Villarreal CF", "ESP"));
            teams.Add(CreateTeam("VfL Wolfsburg", "GER"));
            teams.Add(CreateTeam("BSC Young Boys", "SUI"));
            teams.Add(CreateTeam("FC Zenit", "RUS"));
            return teams;
        }

        public List<Game> GetGames(Dictionary<String, Stadium> stadiums, Dictionary<String, Team> teams)
        {
            var games = new List<Game>();
            games.Add(new Game
            {
                HomeTeamId = teams["BSC Young Boys"].TeamId,
                AwayTeamId = teams["Manchester United"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 9, 14, 19, 45, 0)),
                StadiumId = stadiums["Stadion Wankdorf"].StadiumId
            });
            return games;
        }

        public List<Stadium> GetStadiums()
        {
            var stadiums = new List<Stadium>();
            stadiums.Add(new Stadium() { Name = "Stadion Wankdorf", Capacity = 10000, City = "Berne" });


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
