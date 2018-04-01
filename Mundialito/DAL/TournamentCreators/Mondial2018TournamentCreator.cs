using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;

namespace Mundialito.DAL.DBCreators
{
    public class Mondial2018TournamentCreator : ITournamentCreator
    {
        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            teams.Add(CreateTeam("EGYPT", "EGY"));
            teams.Add(CreateTeam("MOROCCO", "MAR"));
            teams.Add(CreateTeam("NIGERIA", "NGA"));
            teams.Add(CreateTeam("SENEGAL", "SEN"));
            teams.Add(CreateTeam("TUNISIA", "TUN"));
            teams.Add(CreateTeam("AUSTRALIA", "AUS"));
            teams.Add(CreateTeam("IR IRAN", "IRN"));
            teams.Add(CreateTeam("JAPAN", "JPN"));
            teams.Add(CreateTeam("KOREA REPUBLIC", "KOR"));
            teams.Add(CreateTeam("SAUDI ARABIA", "KSA"));
            teams.Add(CreateTeam("BELGIUM", "BEL"));
            teams.Add(CreateTeam("CROATIA", "CRO"));
            teams.Add(CreateTeam("DENMARK", "DEN"));
            teams.Add(CreateTeam("ENGLAND", "ENG"));
            teams.Add(CreateTeam("FRANCE", "FRA"));
            teams.Add(CreateTeam("GERMANY", "GER"));
            teams.Add(CreateTeam("ICELAND", "ISL"));
            teams.Add(CreateTeam("POLAND", "POL"));
            teams.Add(CreateTeam("PORTUGAL", "POR"));
            teams.Add(CreateTeam("RUSSIA", "RUS"));
            teams.Add(CreateTeam("SERBIA", "SRB"));
            teams.Add(CreateTeam("SPAIN", "ESP"));
            teams.Add(CreateTeam("SWEDEN", "SWE"));
            teams.Add(CreateTeam("SWITZERLAND", "SUI"));
            teams.Add(CreateTeam("COSTA RICA", "CRC"));
            teams.Add(CreateTeam("MEXICO", "MEX"));
            teams.Add(CreateTeam("PANAMA", "PAN"));
            teams.Add(CreateTeam("ARGENTINA", "ARG"));
            teams.Add(CreateTeam("BRAZIL", "BRA"));
            teams.Add(CreateTeam("PERU", "PER"));
            teams.Add(CreateTeam("URUGUAY", "URU"));
            teams.Add(CreateTeam("COLOMBIA", "COL"));
            return teams;
        }

        public List<Game> GetGames(Dictionary<String, Stadium> stadiums, Dictionary<String, Team> teams)
        {
            var games = new List<Game>();
            games.Add(new Game
            {
                HomeTeamId = teams["RUSSIA"].TeamId,
                AwayTeamId = teams["SAUDI ARABIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 14, 15 ,0, 0)),
                StadiumId = stadiums["Luzhniki Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["EGYPT"].TeamId,
                AwayTeamId = teams["URUGUAY"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 15, 12, 0, 0)),
                StadiumId = stadiums["Ekaterinburg Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["MOROCCO"].TeamId,
                AwayTeamId = teams["IR IRAN"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 15, 15, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["SPAIN"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 15, 18, 0, 0)),
                StadiumId = stadiums["Fisht Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["FRANCE"].TeamId,
                AwayTeamId = teams["AUSTRALIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 16, 10, 0, 0)),
                StadiumId = stadiums["Kazan Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ARGENTINA"].TeamId,
                AwayTeamId = teams["ICELAND"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 16, 13, 0, 0)),
                StadiumId = stadiums["Spartak Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PERU"].TeamId,
                AwayTeamId = teams["DENMARK"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 16, 16, 0, 0)),
                StadiumId = stadiums["Mordovia Arena"].StadiumId
            });
            

            games.Add(new Game
            {
                HomeTeamId = teams["CROATIA"].TeamId,
                AwayTeamId = teams["NIGERIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 16, 19, 0, 0)),
                StadiumId = stadiums["Kaliningrad Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["COSTA RICA"].TeamId,
                AwayTeamId = teams["SERBIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 17, 12, 0, 0)),
                StadiumId = stadiums["Samara Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GERMANY"].TeamId,
                AwayTeamId = teams["MEXICO"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 17, 15, 0, 0)),
                StadiumId = stadiums["Luzhniki Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BRAZIL"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 17, 18, 0, 0)),
                StadiumId = stadiums["Rostov Arena"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["SWEDEN"].TeamId,
                AwayTeamId = teams["KOREA REPUBLIC"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 18, 12, 0, 0)),
                StadiumId = stadiums["Nizhny Novgorod Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["PANAMA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 18, 15, 0, 0)),
                StadiumId = stadiums["Fisht Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["TUNISIA"].TeamId,
                AwayTeamId = teams["ENGLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 18, 18, 0, 0)),
                StadiumId = stadiums["Volgograd Arena"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["COLOMBIA"].TeamId,
                AwayTeamId = teams["JAPAN"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 19, 12, 0, 0)),
                StadiumId = stadiums["Mordovia Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["POLAND"].TeamId,
                AwayTeamId = teams["SENEGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 19, 15, 0, 0)),
                StadiumId = stadiums["Spartak Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["RUSSIA"].TeamId,
                AwayTeamId = teams["EGYPT"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 19, 18, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["MOROCCO"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 20, 12, 0, 0)),
                StadiumId = stadiums["Luzhniki Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["URUGUAY"].TeamId,
                AwayTeamId = teams["SAUDI ARABIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 20, 15, 0, 0)),
                StadiumId = stadiums["Rostov Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["IR IRAN"].TeamId,
                AwayTeamId = teams["SPAIN"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 20, 18, 0, 0)),
                StadiumId = stadiums["Kazan Arena"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["DENMARK"].TeamId,
                AwayTeamId = teams["AUSTRALIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 21, 12, 0, 0)),
                StadiumId = stadiums["Samara Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["FRANCE"].TeamId,
                AwayTeamId = teams["PERU"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 21, 15, 0, 0)),
                StadiumId = stadiums["Ekaterinburg Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ARGENTINA"].TeamId,
                AwayTeamId = teams["CROATIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 21, 18, 0, 0)),
                StadiumId = stadiums["Nizhny Novgorod Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["BRAZIL"].TeamId,
                AwayTeamId = teams["COSTA RICA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 22, 12, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["NIGERIA"].TeamId,
                AwayTeamId = teams["ICELAND"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 22, 15, 0, 0)),
                StadiumId = stadiums["Volgograd Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SERBIA"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 22, 18, 0, 0)),
                StadiumId = stadiums["Kaliningrad Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["TUNISIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 23, 12, 0, 0)),
                StadiumId = stadiums["Spartak Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["KOREA REPUBLIC"].TeamId,
                AwayTeamId = teams["MEXICO"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 23, 15, 0, 0)),
                StadiumId = stadiums["Rostov Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GERMANY"].TeamId,
                AwayTeamId = teams["SWEDEN"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 23, 18, 0, 0)),
                StadiumId = stadiums["Fisht Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["PANAMA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 24, 12, 0, 0)),
                StadiumId = stadiums["Nizhny Novgorod Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["JAPAN"].TeamId,
                AwayTeamId = teams["SENEGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 24, 15, 0, 0)),
                StadiumId = stadiums["Ekaterinburg Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["POLAND"].TeamId,
                AwayTeamId = teams["COLOMBIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 24, 18, 0, 0)),
                StadiumId = stadiums["Kazan Arena"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["URUGUAY"].TeamId,
                AwayTeamId = teams["RUSSIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 25, 14, 0, 0)),
                StadiumId = stadiums["Samara Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SAUDI ARABIA"].TeamId,
                AwayTeamId = teams["EGYPT"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 25, 14, 0, 0)),
                StadiumId = stadiums["Volgograd Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["IR IRAN"].TeamId,
                AwayTeamId = teams["PORTUGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 25, 18, 0, 0)),
                StadiumId = stadiums["Mordovia Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["MOROCCO"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 25, 18, 0, 0)),
                StadiumId = stadiums["Kaliningrad Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["DENMARK"].TeamId,
                AwayTeamId = teams["FRANCE"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 26, 14, 0, 0)),
                StadiumId = stadiums["Luzhniki Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["AUSTRALIA"].TeamId,
                AwayTeamId = teams["PERU"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 26, 14, 0, 0)),
                StadiumId = stadiums["Fisht Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["NIGERIA"].TeamId,
                AwayTeamId = teams["ARGENTINA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 26, 18, 0, 0)),
                StadiumId = stadiums["Saint Petersburg Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ICELAND"].TeamId,
                AwayTeamId = teams["CROATIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 26, 18, 0, 0)),
                StadiumId = stadiums["Rostov Arena"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["MEXICO"].TeamId,
                AwayTeamId = teams["SWEDEN"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 27, 14, 0, 0)),
                StadiumId = stadiums["Ekaterinburg Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["KOREA REPUBLIC"].TeamId,
                AwayTeamId = teams["GERMANY"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 27, 14, 0, 0)),
                StadiumId = stadiums["Kazan Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SERBIA"].TeamId,
                AwayTeamId = teams["BRAZIL"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 27, 18, 0, 0)),
                StadiumId = stadiums["Spartak Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SWITZERLAND"].TeamId,
                AwayTeamId = teams["COSTA RICA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 27, 18, 0, 0)),
                StadiumId = stadiums["Nizhny Novgorod Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["JAPAN"].TeamId,
                AwayTeamId = teams["POLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 28, 14, 0, 0)),
                StadiumId = stadiums["Volgograd Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SENEGAL"].TeamId,
                AwayTeamId = teams["COLOMBIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 28, 14, 0, 0)),
                StadiumId = stadiums["Samara Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PANAMA"].TeamId,
                AwayTeamId = teams["TUNISIA"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 28, 18, 0, 0)),
                StadiumId = stadiums["Mordovia Arena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["BELGIUM"].TeamId,
                Date = GetFixedDate(new DateTime(2018, 6, 28, 18, 0, 0)),
                StadiumId = stadiums["Kaliningrad Stadium"].StadiumId
            });

            return games;
        }

        public List<Stadium> GetStadiums()
        {
            var stadiums = new List<Stadium>();
            stadiums.Add(new Stadium() { Name = "Kaliningrad Stadium", Capacity = 35000, City = "Kaliningrad" });
            stadiums.Add(new Stadium() { Name = "Volgograd Arena", Capacity = 45000, City = "Volgograd" });
            stadiums.Add(new Stadium() { Name = "Ekaterinburg Arena", Capacity = 35000, City = "Ekaterinburg" });
            stadiums.Add(new Stadium() { Name = "Fisht Stadium", Capacity = 48000, City = "Sochi" });
            stadiums.Add(new Stadium() { Name = "Kazan Arena", Capacity = 45000, City = "Kazan" });
            stadiums.Add(new Stadium() { Name = "Nizhny Novgorod Stadium", Capacity = 45000, City = "Nizhny Novgorod" });
            stadiums.Add(new Stadium() { Name = "Luzhniki Stadium", Capacity = 80000, City = "Moscow" });
            stadiums.Add(new Stadium() { Name = "Samara Arena", Capacity = 45000, City = "Samara" });
            stadiums.Add(new Stadium() { Name = "Rostov Arena", Capacity = 45000, City = "Rostov-on-Don" });
            stadiums.Add(new Stadium() { Name = "Spartak Stadium", Capacity = 45000, City = "Moscow" });
            stadiums.Add(new Stadium() { Name = "Saint Petersburg Stadium", Capacity = 67000, City = "Saint Petersburg" });
            stadiums.Add(new Stadium() { Name = "Mordovia Arena", Capacity = 44000, City = "Saransk" });

            return stadiums;
        }

        private DateTime GetFixedDate(DateTime date)
        {
            return date;
        }

        private Team CreateTeam(String name, String shortName)
        {
            return new Team() { Name = name, ShortName = shortName, Flag = string.Format("http://www.fifa.com/imgml/flags/reflected/m/{0}.png", shortName), Logo = string.Format("http://www.fifa.com/imgml/logos/xs/{0}.gif", shortName) };
        }
    }
}