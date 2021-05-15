using Mundialito.DAL.Games;
using Mundialito.DAL.Players;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;

namespace Mundialito.DAL.DBCreators
{
    public class Euro2021TournamentCreator : ITournamentCreator
    {
        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            teams.Add(CreateTeam("AUSTRIA", "AUT"));
            teams.Add(CreateTeam("BELGIUM", "BEL"));
            teams.Add(CreateTeam("CROATIA", "CRO"));
            teams.Add(CreateTeam("CZECH REPUBLIC", "CZE"));
            teams.Add(CreateTeam("DENMARK", "DEN"));
            teams.Add(CreateTeam("ENGLAND", "ENG"));
            teams.Add(CreateTeam("FINLAND", "FIN"));
            teams.Add(CreateTeam("FRANCE", "FRA"));
            teams.Add(CreateTeam("GERMANY", "GER"));
            teams.Add(CreateTeam("ITALY", "ITA"));
            teams.Add(CreateTeam("HUNGARY", "HUN"));
            teams.Add(CreateTeam("NETHERLANDS", "NED"));
            teams.Add(CreateTeam("NORTH MACEDONIA", "MKD"));
            teams.Add(CreateTeam("POLAND", "POL"));
            teams.Add(CreateTeam("PORTUGAL", "POR"));
            teams.Add(CreateTeam("RUSSIA", "RUS"));
            teams.Add(CreateTeam("SCOTLAND", "SCO"));
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
                HomeTeamId = teams["TURKEY"].TeamId,
                AwayTeamId = teams["ITALY"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 11, 22, 0, 0)),
                StadiumId = stadiums["Olimpico in Rome"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["WALES"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 12, 16, 0, 0)),
                StadiumId = stadiums["Baku Olympic Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["DENMARK"].TeamId,
                AwayTeamId = teams["FINLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 11, 19, 0, 0)),
                StadiumId = stadiums["Parken Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["RUSSIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 11, 22, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["CROATIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 13, 16, 0, 0)),
                StadiumId = stadiums["Wembley Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["AUSTRIA"].TeamId,
                AwayTeamId = teams["NORTH MACEDONIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 13, 19, 0, 0)),
                StadiumId = stadiums["National Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["NETHERLANDS"].TeamId,
                AwayTeamId = teams["UKRAINE"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 13, 22, 0, 0)),
                StadiumId = stadiums["Johan Cruijff ArenA"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["SCOTLAND"].TeamId,
                AwayTeamId = teams["CZECH REPUBLIC"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 14, 16, 0, 0)),
                StadiumId = stadiums["Hampden Park"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["POLAND"].TeamId,
                AwayTeamId = teams["SLOVAKIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 14, 19, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["SWEDEN"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 14, 22, 0, 0)),
                StadiumId = stadiums["Stadium La Cartuja Sevilla"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["HUNGARY"].TeamId,
                AwayTeamId = teams["PORTUGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 15, 19, 0, 0)),
                StadiumId = stadiums["Puskas Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["FRANCE"].TeamId,
                AwayTeamId = teams["GERMANY"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 15, 22, 0, 0)),
                StadiumId = stadiums["Football Arena Munich"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["FINLAND"].TeamId,
                AwayTeamId = teams["RUSSIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 16, 16, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["TURKEY"].TeamId,
                AwayTeamId = teams["WALES"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 16, 19, 0, 0)),
                StadiumId = stadiums["Baku Olympic Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ITALY"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 16, 22, 0, 0)),
                StadiumId = stadiums["Olimpico in Rome"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["UKRAINE"].TeamId,
                AwayTeamId = teams["NORTH MACEDONIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 17, 16, 0, 0)),
                StadiumId = stadiums["National Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["DENMARK"].TeamId,
                AwayTeamId = teams["BELGIUM"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 17, 19, 0, 0)),
                StadiumId = stadiums["Parken Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["NETHERLANDS"].TeamId,
                AwayTeamId = teams["AUSTRIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 17, 22, 0, 0)),
                StadiumId = stadiums["Johan Cruijff ArenA"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["SWEDEN"].TeamId,
                AwayTeamId = teams["SLOVAKIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 18, 16, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CROATIA"].TeamId,
                AwayTeamId = teams["CZECH REPUBLIC"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 18, 19, 0, 0)),
                StadiumId = stadiums["Hampden Park"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["SCOTLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 18, 22, 0, 0)),
                StadiumId = stadiums["Wembley Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["HUNGARY"].TeamId,
                AwayTeamId = teams["FRANCE"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 19, 16, 0, 0)),
                StadiumId = stadiums["Puskas Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["GERMANY"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 19, 19, 0, 0)),
                StadiumId = stadiums["Football Arena Munich"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["POLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 19, 22, 0, 0)),
                StadiumId = stadiums["Stadium La Cartuja Sevilla"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ITALY"].TeamId,
                AwayTeamId = teams["WALES"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 20, 19, 0, 0)),
                StadiumId = stadiums["Olimpico in Rome"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SWITZERLAND"].TeamId,
                AwayTeamId = teams["TURKEY"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 20, 19, 0, 0)),
                StadiumId = stadiums["Baku Olympic Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["UKRAINE"].TeamId,
                AwayTeamId = teams["AUSTRIA"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 21, 19, 0, 0)),
                StadiumId = stadiums["National Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["NORTH MACEDONIA"].TeamId,
                AwayTeamId = teams["NETHERLANDS"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 21, 19, 0, 0)),
                StadiumId = stadiums["Johan Cruijff ArenA"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["FINLAND"].TeamId,
                AwayTeamId = teams["BELGIUM"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 21, 22, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["RUSSIA"].TeamId,
                AwayTeamId = teams["DENMARK"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 21, 22, 0, 0)),
                StadiumId = stadiums["Parken Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["CZECH REPUBLIC"].TeamId,
                AwayTeamId = teams["ENGLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 22, 22, 0, 0)),
                StadiumId = stadiums["Wembley Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CROATIA"].TeamId,
                AwayTeamId = teams["SCOTLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 22, 22, 0, 0)),
                StadiumId = stadiums["Hampden Park"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["SWEDEN"].TeamId,
                AwayTeamId = teams["POLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 23, 19, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SLOVAKIA"].TeamId,
                AwayTeamId = teams["SPAIN"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 23, 19, 0, 0)),
                StadiumId = stadiums["Stadium La Cartuja Sevilla"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GERMANY"].TeamId,
                AwayTeamId = teams["HUNGARY"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 23, 22, 0, 0)),
                StadiumId = stadiums["Football Arena Munich"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["FRANCE"].TeamId,
                Date = GetFixedDate(new DateTime(2021, 6, 23, 22, 0, 0)),
                StadiumId = stadiums["Puskas Arena"].StadiumId
            });

            /* */
            return games;
        }

        public List<Stadium> GetStadiums()
        {
            var stadiums = new List<Stadium>();
            stadiums.Add(new Stadium() { Name = "Olimpico in Rome", Capacity = 70634, City = "Rome" });
            stadiums.Add(new Stadium() { Name = "Johan Cruijff ArenA", Capacity = 55500, City = "Amsterdam" });
            stadiums.Add(new Stadium() { Name = "Baku Olympic Stadium", Capacity = 69870, City = "Baku" });
            stadiums.Add(new Stadium() { Name = "National Arena", Capacity = 55634, City = "Bucharest" });
            stadiums.Add(new Stadium() { Name = "Puskas Arena", Capacity = 67215, City = "Budapest" });
            stadiums.Add(new Stadium() { Name = "Parken Stadium", Capacity = 38065, City = "Copenhagen" });
            stadiums.Add(new Stadium() { Name = "Hampden Park", Capacity = 51866, City = "Glasgow" });
            stadiums.Add(new Stadium() { Name = "Wembley Stadium", Capacity = 90000, City = "London" });
            stadiums.Add(new Stadium() { Name = "Football Arena Munich", Capacity = 70000, City = "Munich" });
            stadiums.Add(new Stadium() { Name = "Saint Petersburg Stadium", Capacity = 68000, City = "Saint Petersburg" });
            stadiums.Add(new Stadium() { Name = "Stadium La Cartuja Sevilla", Capacity = 60000, City = "Seville" });
            return stadiums;
        }

        public List<Player> GetPlayers(Dictionary<String, Team> teams)
        {
            var players = new List<Player>();
            players.Add(new Player() { Name = "aa", TeamId = teams["TURKEY"].TeamId });
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