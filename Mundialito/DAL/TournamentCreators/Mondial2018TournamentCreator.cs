using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                HomeTeamId = teams["BRAZIL"].TeamId,
                AwayTeamId = teams["CROATIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 12, 20, 0, 0)),
                StadiumId = stadiums["Arena de Sao Paulo"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["MEXICO"].TeamId,
                AwayTeamId = teams["CAMEROON"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 13, 16, 0, 0)),
                StadiumId = stadiums["Estadio das Dunas"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["NETHERLANDS"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 13, 19, 0, 0)),
                StadiumId = stadiums["Arena Fonte Nova"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CHILE"].TeamId,
                AwayTeamId = teams["AUSTRALIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 13, 22, 0, 0)),
                StadiumId = stadiums["Arena Pantanal"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["COLOMBIA"].TeamId,
                AwayTeamId = teams["GREECE"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 14, 16, 0, 0)),
                StadiumId = stadiums["Estadio Mineirao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["URUGUAY"].TeamId,
                AwayTeamId = teams["COSTA RICA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 14, 19, 0, 0)),
                StadiumId = stadiums["Estadio Castelao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["ITALY"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 14, 22, 0, 0)),
                StadiumId = stadiums["Arena Amazonia"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["CÔTE D'IVOIRE"].TeamId,
                AwayTeamId = teams["JAPAN"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 15, 1, 0, 0)),
                StadiumId = stadiums["Arena Pernambuco"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SWITZERLAND"].TeamId,
                AwayTeamId = teams["ECUADOR"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 15, 16, 0, 0)),
                StadiumId = stadiums["Estadio Nacional de Brasilia"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["FRANCE"].TeamId,
                AwayTeamId = teams["HONDURAS"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 15, 19, 0, 0)),
                StadiumId = stadiums["Estadio Beira-Rio"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ARGENTINA"].TeamId,
                AwayTeamId = teams["BOSNIA AND HERZEGOVINA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 15, 22, 0, 0)),
                StadiumId = stadiums["Estadio Do Maracana"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["GERMANY"].TeamId,
                AwayTeamId = teams["PORTUGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 16, 16, 0, 0)),
                StadiumId = stadiums["Arena Fonte Nova"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["IRAN"].TeamId,
                AwayTeamId = teams["NIGERIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 16, 19, 0, 0)),
                StadiumId = stadiums["Arena da Baixada"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GHANA"].TeamId,
                AwayTeamId = teams["USA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 16, 22, 0, 0)),
                StadiumId = stadiums["Estadio das Dunas"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["ALGERIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 17, 16, 0, 0)),
                StadiumId = stadiums["Estadio Mineirao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BRAZIL"].TeamId,
                AwayTeamId = teams["MEXICO"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 17, 19, 0, 0)),
                StadiumId = stadiums["Estadio Castelao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["RUSSIA"].TeamId,
                AwayTeamId = teams["KOREA REPUBLIC"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 17, 22, 0, 0)),
                StadiumId = stadiums["Estadio das Dunas"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["AUSTRALIA"].TeamId,
                AwayTeamId = teams["NETHERLANDS"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 18, 16, 0, 0)),
                StadiumId = stadiums["Estadio Beira-Rio"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["CHILE"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 18, 19, 0, 0)),
                StadiumId = stadiums["Estadio Do Maracana"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CAMEROON"].TeamId,
                AwayTeamId = teams["CROATIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 18, 22, 0, 0)),
                StadiumId = stadiums["Arena Amazonia"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["COLOMBIA"].TeamId,
                AwayTeamId = teams["CÔTE D'IVOIRE"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 19, 16, 0, 0)),
                StadiumId = stadiums["Estadio Nacional de Brasilia"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["URUGUAY"].TeamId,
                AwayTeamId = teams["ENGLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 19, 19, 0, 0)),
                StadiumId = stadiums["Arena de Sao Paulo"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["JAPAN"].TeamId,
                AwayTeamId = teams["GREECE"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 19, 22, 0, 0)),
                StadiumId = stadiums["Estadio das Dunas"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ITALY"].TeamId,
                AwayTeamId = teams["COSTA RICA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 20, 16, 0, 0)),
                StadiumId = stadiums["Arena Pernambuco"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SWITZERLAND"].TeamId,
                AwayTeamId = teams["FRANCE"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 20, 19, 0, 0)),
                StadiumId = stadiums["Arena Fonte Nova"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["HONDURAS"].TeamId,
                AwayTeamId = teams["ECUADOR"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 20, 22, 0, 0)),
                StadiumId = stadiums["Arena da Baixada"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ARGENTINA"].TeamId,
                AwayTeamId = teams["IRAN"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 21, 16, 0, 0)),
                StadiumId = stadiums["Estadio Mineirao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GERMANY"].TeamId,
                AwayTeamId = teams["GHANA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 21, 19, 0, 0)),
                StadiumId = stadiums["Estadio Castelao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["NIGERIA"].TeamId,
                AwayTeamId = teams["BOSNIA AND HERZEGOVINA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 21, 22, 0, 0)),
                StadiumId = stadiums["Arena Pantanal"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["RUSSIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 22, 16, 0, 0)),
                StadiumId = stadiums["Estadio Mineirao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["KOREA REPUBLIC"].TeamId,
                AwayTeamId = teams["ALGERIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 22, 19, 0, 0)),
                StadiumId = stadiums["Estadio Beira-Rio"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["USA"].TeamId,
                AwayTeamId = teams["PORTUGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 22, 22, 0, 0)),
                StadiumId = stadiums["Estadio Do Maracana"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["NETHERLANDS"].TeamId,
                AwayTeamId = teams["CHILE"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 23, 16, 0, 0)),
                StadiumId = stadiums["Arena de Sao Paulo"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["AUSTRALIA"].TeamId,
                AwayTeamId = teams["SPAIN"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 23, 16, 0, 0)),
                StadiumId = stadiums["Arena da Baixada"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CAMEROON"].TeamId,
                AwayTeamId = teams["BRAZIL"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 23, 20, 0, 0)),
                StadiumId = stadiums["Estadio Nacional de Brasilia"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CROATIA"].TeamId,
                AwayTeamId = teams["MEXICO"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 23, 20, 0, 0)),
                StadiumId = stadiums["Arena Pernambuco"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ITALY"].TeamId,
                AwayTeamId = teams["URUGUAY"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 24, 16, 0, 0)),
                StadiumId = stadiums["Estadio Mineirao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["COSTA RICA"].TeamId,
                AwayTeamId = teams["ENGLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 24, 16, 0, 0)),
                StadiumId = stadiums["Estadio Mineirao"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["JAPAN"].TeamId,
                AwayTeamId = teams["COLOMBIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 24, 20, 0, 0)),
                StadiumId = stadiums["Arena Pantanal"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GREECE"].TeamId,
                AwayTeamId = teams["CÔTE D'IVOIRE"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 24, 20, 0, 0)),
                StadiumId = stadiums["Estadio Castelao"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["NIGERIA"].TeamId,
                AwayTeamId = teams["ARGENTINA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 25, 16, 0, 0)),
                StadiumId = stadiums["Estadio Beira-Rio"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BOSNIA AND HERZEGOVINA"].TeamId,
                AwayTeamId = teams["IRAN"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 25, 16, 0, 0)),
                StadiumId = stadiums["Arena Fonte Nova"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["HONDURAS"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 25, 20, 0, 0)),
                StadiumId = stadiums["Arena Amazonia"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ECUADOR"].TeamId,
                AwayTeamId = teams["FRANCE"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 25, 20, 0, 0)),
                StadiumId = stadiums["Estadio Do Maracana"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["GHANA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 26, 16, 0, 0)),
                StadiumId = stadiums["Estadio Nacional de Brasilia"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["USA"].TeamId,
                AwayTeamId = teams["GERMANY"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 26, 16, 0, 0)),
                StadiumId = stadiums["Arena Pernambuco"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["KOREA REPUBLIC"].TeamId,
                AwayTeamId = teams["BELGIUM"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 26, 20, 0, 0)),
                StadiumId = stadiums["Arena de Sao Paulo"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ALGERIA"].TeamId,
                AwayTeamId = teams["RUSSIA"].TeamId,
                Date = GetFixedDate(new DateTime(2014, 6, 26, 20, 0, 0)),
                StadiumId = stadiums["Arena da Baixada"].StadiumId
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
            //return date;
            return date.AddDays(65);
        }

        private Team CreateTeam(String name, String shortName)
        {
            return new Team() { Name = name, ShortName = shortName, Flag = string.Format("http://www.fifa.com/imgml/flags/reflected/m/{0}.png", shortName), Logo = string.Format("http://www.fifa.com/imgml/logos/xs/{0}.gif", shortName) };
        }
    }
}