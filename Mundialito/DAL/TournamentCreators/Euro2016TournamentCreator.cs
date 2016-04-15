using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.DAL.DBCreators
{
    public class Euro2016TournamentCreator : ITournamentCreator
    {
        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            teams.Add(CreateTeam("ALBANIA", "ALB"));
            teams.Add(CreateTeam("AUSTRIA", "AUT"));
            teams.Add(CreateTeam("BELGIUM", "BEL"));
            teams.Add(CreateTeam("CROATIA", "CRO"));
            teams.Add(CreateTeam("CZECH REPUBLIC", "CZE"));
            teams.Add(CreateTeam("ENGLAND", "ENG"));
            teams.Add(CreateTeam("FRANCE", "FRA"));
            teams.Add(CreateTeam("GERMANY", "GER"));
            teams.Add(CreateTeam("HUNGARY", "HUN"));
            teams.Add(CreateTeam("ICELAND", "ISL"));
            teams.Add(CreateTeam("ITALY", "ITA"));
            teams.Add(CreateTeam("NORTHERN IRELAND", "NIR"));
            teams.Add(CreateTeam("POLAND", "POL"));
            teams.Add(CreateTeam("PORTUGAL", "POR"));
            teams.Add(CreateTeam("REPUBLIC OF IRELAND", "IRL"));
            teams.Add(CreateTeam("ROMANIA", "ROU"));
            teams.Add(CreateTeam("RUSSIA", "RUS"));
            teams.Add(CreateTeam("SLOVAKIA", "SVK"));
            teams.Add(CreateTeam("SPAIN", "ESP"));
            teams.Add(CreateTeam("SWEDEN", "SWE"));
            teams.Add(CreateTeam("SWITZERLAND", "SUI"));
            teams.Add(CreateTeam("TURKEY", "TUR"));
            teams.Add(CreateTeam("UKRAINE", "UKR"));
            teams.Add(CreateTeam("WALES", "WAL"));
            
            return teams;
        }

        public List<Game> GetGames(Dictionary<String, Stadium> stadiums, Dictionary<String, Team> teams)
        {
            var games = new List<Game>();
            games.Add(new Game
            {
                HomeTeamId = teams["FRANCE"].TeamId,
                AwayTeamId = teams["ROMANIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 10, 22, 0, 0)),
                StadiumId = stadiums["Stade de France"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ALBANIA"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 11, 16, 0, 0)),
                StadiumId = stadiums["Stade Bollaert-Delelis"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["WALES"].TeamId,
                AwayTeamId = teams["SLOVAKIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 11, 19, 0, 0)),
                StadiumId = stadiums["Stade de Bordeaux"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["RUSSIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 11, 22, 0, 0)),
                StadiumId = stadiums["Stade Vélodrome"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["TURKEY"].TeamId,
                AwayTeamId = teams["CROATIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 12, 16, 0, 0)),
                StadiumId = stadiums["Parc des Princes"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["POLAND"].TeamId,
                AwayTeamId = teams["NORTHERN IRELAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 12, 19, 0, 0)),
                StadiumId = stadiums["Stade de Nice"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GERMANY"].TeamId,
                AwayTeamId = teams["UKRAINE"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 12, 22, 0, 0)),
                StadiumId = stadiums["Stade Pierre Mauroy"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["CZECH REPUBLIC"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 13, 16, 0, 0)),
                StadiumId = stadiums["Stadium de Toulouse"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["REPUBLIC OF IRELAND"].TeamId,
                AwayTeamId = teams["SWEDEN"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 13, 19, 0, 0)),
                StadiumId = stadiums["Stade de France"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["ITALY"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 13, 22, 0, 0)),
                StadiumId = stadiums["Stade de Lyon"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["AUSTRIA"].TeamId,
                AwayTeamId = teams["HUNGARY"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 14, 19, 0, 0)),
                StadiumId = stadiums["Stade de Bordeaux"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["ICELAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 14, 22, 0, 0)),
                StadiumId = stadiums["Stade Geoffroy Guichard"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["RUSSIA"].TeamId,
                AwayTeamId = teams["SLOVAKIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 15, 16, 0, 0)),
                StadiumId = stadiums["Stade Pierre Mauroy"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ROMANIA"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 15, 19, 0, 0)),
                StadiumId = stadiums["Parc des Princes"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["FRANCE"].TeamId,
                AwayTeamId = teams["ALBANIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 15, 22, 0, 0)),
                StadiumId = stadiums["Stade Vélodrome"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["WALES"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 16, 16, 0, 0)),
                StadiumId = stadiums["Stade Bollaert-Delelis"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["UKRAINE"].TeamId,
                AwayTeamId = teams["NORTHERN IRELAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 16, 19, 0, 0)),
                StadiumId = stadiums["Stade de Lyon"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GERMANY"].TeamId,
                AwayTeamId = teams["POLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 16, 22, 0, 0)),
                StadiumId = stadiums["Stade de France"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ITALY"].TeamId,
                AwayTeamId = teams["SWEDEN"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 17, 16, 0, 0)),
                StadiumId = stadiums["Stadium de Toulouse"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CZECH REPUBLIC"].TeamId,
                AwayTeamId = teams["CROATIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 17, 19, 0, 0)),
                StadiumId = stadiums["Stade Geoffroy Guichard"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["TURKEY"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 17, 22, 0, 0)),
                StadiumId = stadiums["Stade de Nice"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["REPUBLIC OF IRELAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 18, 16, 0, 0)),
                StadiumId = stadiums["Stade de Bordeaux"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ICELAND"].TeamId,
                AwayTeamId = teams["HUNGARY"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 18, 19, 0, 0)),
                StadiumId = stadiums["Stade Vélodrome"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["AUSTRIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 18, 22, 0, 0)),
                StadiumId = stadiums["Parc des Princes"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["SWITZERLAND"].TeamId,
                AwayTeamId = teams["FRANCE"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 19, 22, 0, 0)),
                StadiumId = stadiums["Stade Pierre Mauroy"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ROMANIA"].TeamId,
                AwayTeamId = teams["ALBANIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 19, 22, 0, 0)),
                StadiumId = stadiums["Stade de Lyon"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["SLOVAKIA"].TeamId,
                AwayTeamId = teams["ENGLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 20, 22, 0, 0)),
                StadiumId = stadiums["Stade Geoffroy Guichard"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["RUSSIA"].TeamId,
                AwayTeamId = teams["WALES"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 20, 22, 0, 0)),
                StadiumId = stadiums["Stadium de Toulouse"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["NORTHERN IRELAND"].TeamId,
                AwayTeamId = teams["GERMANY"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 21, 19, 0, 0)),
                StadiumId = stadiums["Parc des Princes"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["UKRAINE"].TeamId,
                AwayTeamId = teams["POLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 21, 19, 0, 0)),
                StadiumId = stadiums["Stade Vélodrome"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CROATIA"].TeamId,
                AwayTeamId = teams["SPAIN"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 21, 22, 0, 0)),
                StadiumId = stadiums["Stade de Bordeaux"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CZECH REPUBLIC"].TeamId,
                AwayTeamId = teams["TURKEY"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 21, 22, 0, 0)),
                StadiumId = stadiums["Stade Bollaert-Delelis"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ICELAND"].TeamId,
                AwayTeamId = teams["AUSTRIA"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 22, 19, 0, 0)),
                StadiumId = stadiums["Stade de France"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["HUNGARY"].TeamId,
                AwayTeamId = teams["PORTUGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 22, 19, 0, 0)),
                StadiumId = stadiums["Stade de Lyon"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SWEDEN"].TeamId,
                AwayTeamId = teams["BELGIUM"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 22, 22, 0, 0)),
                StadiumId = stadiums["Stade de Nice"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ITALY"].TeamId,
                AwayTeamId = teams["REPUBLIC OF IRELAND"].TeamId,
                Date = GetFixedDate(new DateTime(2016, 6, 22, 22, 0, 0)),
                StadiumId = stadiums["Stade Pierre Mauroy"].StadiumId
            });

            /* */
            return games;
        }

        public List<Stadium> GetStadiums()
        {
            var stadiums = new List<Stadium>();
            stadiums.Add(new Stadium() { Name = "Parc des Princes", Capacity = 45000, City = "Paris" });
            stadiums.Add(new Stadium() { Name = "Stade de France", Capacity = 80000, City = "Saint-Denis" });
            stadiums.Add(new Stadium() { Name = "Stade de Bordeaux", Capacity = 42000, City = "Bordeaux" });
            stadiums.Add(new Stadium() { Name = "Stade Bollaert-Delelis", Capacity = 35000, City = "Lens" });
            stadiums.Add(new Stadium() { Name = "Stade Pierre Mauroy", Capacity = 50000, City = "Lille" });
            stadiums.Add(new Stadium() { Name = "Stade de Lyon", Capacity = 59000, City = "Lyon" });
            stadiums.Add(new Stadium() { Name = "Stade Vélodrome", Capacity = 67000, City = "Marseille" });
            stadiums.Add(new Stadium() { Name = "Stade de Nice", Capacity = 35000, City = "Nice" });
            stadiums.Add(new Stadium() { Name = "Stade Geoffroy Guichard", Capacity = 42000, City = "Saint-Etienne" });
            stadiums.Add(new Stadium() { Name = "Stadium de Toulouse", Capacity = 33000, City = "Toulouse" });
            return stadiums;
        }

        private DateTime GetFixedDate(DateTime date)
        {
            return date.Subtract(TimeSpan.FromHours(3));
            //return date.AddDays(65);
        }

        private Team CreateTeam(String name, String shortName)
        {
            return new Team() { Name = name, ShortName = shortName, Flag = string.Format("http://www.fifa.com/imgml/flags/reflected/m/{0}.png", shortName), Logo = string.Format("http://img.fifa.com/images/flags/2/{0}.png", shortName) };
        }
    }
}