using Mundialito.DAL.Games;
using Mundialito.DAL.Players;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;

namespace Mundialito.DAL.DBCreators
{
    public class Mundial2023TournamentCreator : ITournamentCreator
    {
        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            teams.Add(CreateTeam("ARGENTINA", "ARG"));
            teams.Add(CreateTeam("AUSTRALIA", "AUS"));
            teams.Add(CreateTeam("BELGIUM", "BEL"));
            teams.Add(CreateTeam("BRAZIL", "BRA"));
            teams.Add(CreateTeam("CAMERON", "CMR"));
            teams.Add(CreateTeam("CANADA", "CAN"));
            teams.Add(CreateTeam("COSTA RICA", "CRC"));
            teams.Add(CreateTeam("CROATIA", "CRO"));
            teams.Add(CreateTeam("CZECH REPUBLIC", "CZE"));
            teams.Add(CreateTeam("DENMARK", "DEN"));
            teams.Add(CreateTeam("ECUADOR", "ECU"));
            teams.Add(CreateTeam("ENGLAND", "ENG"));
            teams.Add(CreateTeam("FRANCE", "FRA"));
            teams.Add(CreateTeam("GERMANY", "GER"));
            teams.Add(CreateTeam("GHANA", "GHA"));
            teams.Add(CreateTeam("IRAN", "IRN"));
            teams.Add(CreateTeam("JAPAN", "JPN"));
            teams.Add(CreateTeam("KOREA REPUBLIC", "KOR"));
            teams.Add(CreateTeam("MEXICO", "MEX"));
            teams.Add(CreateTeam("MOROCCO", "MAR"));
            teams.Add(CreateTeam("NETHERLANDS", "NED"));
            teams.Add(CreateTeam("POLAND", "POL"));
            teams.Add(CreateTeam("PORTUGAL", "POR"));
            teams.Add(CreateTeam("QATAR", "QAT"));
            teams.Add(CreateTeam("SAUDI ARABIA", "KSA"));
            teams.Add(CreateTeam("SENEGAL", "SEN"));
            teams.Add(CreateTeam("SERBIA", "SRB"));
            teams.Add(CreateTeam("SPAIN", "ESP"));
            teams.Add(CreateTeam("SWITZERLAND", "SUI"));
            teams.Add(CreateTeam("TUNISIA", "TUN"));
            teams.Add(CreateTeam("URUGUAY", "URU"));
            teams.Add(CreateTeam("USA", "USA"));
            teams.Add(CreateTeam("WALES", "WAL"));
            
            return teams;
        }

        public List<Game> GetGames(Dictionary<String, Stadium> stadiums, Dictionary<String, Team> teams)
        {
            var games = new List<Game>();
            games.Add(new Game
            {
                HomeTeamId = teams["QATAR"].TeamId,
                AwayTeamId = teams["ECUADOR"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 20, 18, 0, 0)),
                StadiumId = stadiums["Al Bayt Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["IRAN"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 21, 15, 0, 0)),
                StadiumId = stadiums["Khalifa International Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SENEGAL"].TeamId,
                AwayTeamId = teams["NETHERLANDS"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 21, 18, 0, 0)),
                StadiumId = stadiums["Al Thumama Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["USA"].TeamId,
                AwayTeamId = teams["WALES"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 21, 21, 0, 0)),
                StadiumId = stadiums["Ahmad Bin Ali Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ARGENTINA"].TeamId,
                AwayTeamId = teams["SAUDI ARABIA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 22, 12, 0, 0)),
                StadiumId = stadiums["Lusail Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["DENMARK"].TeamId,
                AwayTeamId = teams["TUNISIA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 22, 15, 0, 0)),
                StadiumId = stadiums["Education City Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["MEXICO"].TeamId,
                AwayTeamId = teams["POLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 22, 18, 0, 0)),
                StadiumId = stadiums["Stadium 974"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["FRANCE"].TeamId,
                AwayTeamId = teams["AUSTRALIA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 22, 21, 0, 0)),
                StadiumId = stadiums["Al Janoub Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["MOROCCO"].TeamId,
                AwayTeamId = teams["CROATIA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 23, 12, 0, 0)),
                StadiumId = stadiums["Al Bayt Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GERMANY"].TeamId,
                AwayTeamId = teams["JAPAN"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 23, 15, 0, 0)),
                StadiumId = stadiums["Khalifa International Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["COSTA RICA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 23, 18, 0, 0)),
                StadiumId = stadiums["Al Thumama Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["CANADA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 23, 21, 0, 0)),
                StadiumId = stadiums["Ahmad Bin Ali Stadium"].StadiumId
            });


            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["SWITZERLAND"].TeamId,
                AwayTeamId = teams["CAMERON"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 24, 12, 0, 0)),
                StadiumId = stadiums["Al Janoub Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["URUGUAY"].TeamId,
                AwayTeamId = teams["KOREA REPUBLIC"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 24, 15, 0, 0)),
                StadiumId = stadiums["Education City Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["GHANA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 24, 18, 0, 0)),
                StadiumId = stadiums["Stadium 974"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BRAZIL"].TeamId,
                AwayTeamId = teams["SERBIA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 24, 21, 0, 0)),
                StadiumId = stadiums["Lusail Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["WALES"].TeamId,
                AwayTeamId = teams["IRAN"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 25, 12, 0, 0)),
                StadiumId = stadiums["Ahmad Bin Ali Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["QATAR"].TeamId,
                AwayTeamId = teams["SENEGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 25, 15, 0, 0)),
                StadiumId = stadiums["Al Thumama Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["NETHERLANDS"].TeamId,
                AwayTeamId = teams["ECUADOR"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 25, 18, 0, 0)),
                StadiumId = stadiums["Khalifa International Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ENGLAND"].TeamId,
                AwayTeamId = teams["USA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 25, 21, 0, 0)),
                StadiumId = stadiums["Al Bayt Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["TUNISIA"].TeamId,
                AwayTeamId = teams["AUSTRALIA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 26, 12, 0, 0)),
                StadiumId = stadiums["Al Janoub Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["POLAND"].TeamId,
                AwayTeamId = teams["SAUDI ARABIA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 26, 15, 0, 0)),
                StadiumId = stadiums["Education City Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["FRANCE"].TeamId,
                AwayTeamId = teams["DENMARK"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 26, 18, 0, 0)),
                StadiumId = stadiums["Stadium 974"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["ARGENTINA"].TeamId,
                AwayTeamId = teams["MEXICO"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 26, 21, 0, 0)),
                StadiumId = stadiums["Lusail Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["JAPAN"].TeamId,
                AwayTeamId = teams["COSTA RICA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 27, 12, 0, 0)),
                StadiumId = stadiums["Ahmad Bin Ali Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BELGIUM"].TeamId,
                AwayTeamId = teams["MOROCCO"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 27, 15, 0, 0)),
                StadiumId = stadiums["Al Thumama Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CROATIA"].TeamId,
                AwayTeamId = teams["CANADA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 27, 18, 0, 0)),
                StadiumId = stadiums["Khalifa International Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SPAIN"].TeamId,
                AwayTeamId = teams["GERMANY"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 27, 21, 0, 0)),
                StadiumId = stadiums["Al Bayt Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["CAMERON"].TeamId,
                AwayTeamId = teams["SERBIA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 28, 12, 0, 0)),
                StadiumId = stadiums["Al Janoub Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["KOREA REPUBLIC"].TeamId,
                AwayTeamId = teams["GHANA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 28, 15, 0, 0)),
                StadiumId = stadiums["Education City Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["BRAZIL"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 28, 18, 0, 0)),
                StadiumId = stadiums["Stadium 974"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["PORTUGAL"].TeamId,
                AwayTeamId = teams["URUGUAY"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 28, 21, 0, 0)),
                StadiumId = stadiums["Lusail Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ECUADOR"].TeamId,
                AwayTeamId = teams["SENEGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 29, 17, 0, 0)),
                StadiumId = stadiums["Khalifa International Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["NETHERLANDS"].TeamId,
                AwayTeamId = teams["QATAR"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 29, 17, 0, 0)),
                StadiumId = stadiums["Al Bayt Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["IRAN"].TeamId,
                AwayTeamId = teams["USA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 29, 21, 0, 0)),
                StadiumId = stadiums["Al Thumama Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["WALES"].TeamId,
                AwayTeamId = teams["ENGLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 29, 21, 0, 0)),
                StadiumId = stadiums["Ahmad Bin Ali Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["TUNISIA"].TeamId,
                AwayTeamId = teams["FRANCE"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 30, 17, 0, 0)),
                StadiumId = stadiums["Education City Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["AUSTRALIA"].TeamId,
                AwayTeamId = teams["DENMARK"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 30, 17, 0, 0)),
                StadiumId = stadiums["Al Janoub Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["POLAND"].TeamId,
                AwayTeamId = teams["ARGENTINA"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 30, 21, 0, 0)),
                StadiumId = stadiums["Stadium 974"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SAUDI ARABIA"].TeamId,
                AwayTeamId = teams["MEXICO"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 11, 30, 21, 0, 0)),
                StadiumId = stadiums["Lusail Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["CROATIA"].TeamId,
                AwayTeamId = teams["BELGIUM"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 12, 1, 17, 0, 0)),
                StadiumId = stadiums["Ahmad Bin Ali Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CANADA"].TeamId,
                AwayTeamId = teams["MOROCCO"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 12, 1, 17, 0, 0)),
                StadiumId = stadiums["Al Thumama Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["JAPAN"].TeamId,
                AwayTeamId = teams["SPAIN"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 12, 1, 21, 0, 0)),
                StadiumId = stadiums["Khalifa International Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["COSTA RICA"].TeamId,
                AwayTeamId = teams["GERMANY"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 12, 1, 21, 0, 0)),
                StadiumId = stadiums["Al Bayt Stadium"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["KOREA REPUBLIC"].TeamId,
                AwayTeamId = teams["PORTUGAL"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 12, 2, 17, 0, 0)),
                StadiumId = stadiums["Education City Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["GHANA"].TeamId,
                AwayTeamId = teams["URUGUAY"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 12, 2, 17, 0, 0)),
                StadiumId = stadiums["Al Janoub Stadium"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["SERBIA"].TeamId,
                AwayTeamId = teams["SWITZERLAND"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 12, 2, 21, 0, 0)),
                StadiumId = stadiums["Stadium 974"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CAMERON"].TeamId,
                AwayTeamId = teams["BRAZIL"].TeamId,
                Date = GetFixedDate(new DateTime(2022, 12, 2, 21, 0, 0)),
                StadiumId = stadiums["Lusail Stadium"].StadiumId
            });

            /* */
            return games;
        }

        public List<Stadium> GetStadiums()
        {
            var stadiums = new List<Stadium>();
            stadiums.Add(new Stadium() { Name = "Al Bayt Stadium", Capacity = 60000, City = "Al Khor" });
            stadiums.Add(new Stadium() { Name = "Lusail Stadium", Capacity = 80000, City = "Lusail" });
            stadiums.Add(new Stadium() { Name = "Al Janoub Stadium", Capacity = 40000, City = "Al Wakrah" });
            stadiums.Add(new Stadium() { Name = "Ahmad Bin Ali Stadium", Capacity = 40000, City = "Al Rayyan" });
            stadiums.Add(new Stadium() { Name = "Khalifa International Stadium", Capacity = 40000, City = "Doha" });
            stadiums.Add(new Stadium() { Name = "Education City Stadium", Capacity = 40000, City = "Doha" });
            stadiums.Add(new Stadium() { Name = "Stadium 974", Capacity = 40000, City = "Doha" });
            stadiums.Add(new Stadium() { Name = "Al Thumama Stadium", Capacity = 40000, City = "Doha" });
            return stadiums;
        }

        public List<Player> GetPlayers()
        {
            var players = new List<Player>();
            players.Add(new Player() { Name = "Harry Kane" });
            players.Add(new Player() { Name = "Kylian Mbappe" });
            players.Add(new Player() { Name = "Karim Benzema" });
            players.Add(new Player() { Name = "Lionel Messi" });
            players.Add(new Player() { Name = "Neymar" });
            players.Add(new Player() { Name = "Cristiano Ronaldo" });
            players.Add(new Player() { Name = "Romelu Lukaku" });
            players.Add(new Player() { Name = "Vinicius Junior" });
            players.Add(new Player() { Name = "Memphis Depay" });
            players.Add(new Player() { Name = "Lautaro Martinez" });
            players.Add(new Player() { Name = "Diogo Jota" });
            players.Add(new Player() { Name = "Gabriel Jesus" });
            players.Add(new Player() { Name = "Timo Werner" });
            players.Add(new Player() { Name = "Alvaro Morata" });
            players.Add(new Player() { Name = "Antoine Griezmann" });
            players.Add(new Player() { Name = "Darwin Nunez" });
            players.Add(new Player() { Name = "Raheem Sterling" });
            players.Add(new Player() { Name = "Robert Lewandowski" });
            players.Add(new Player() { Name = "Kai Havertz" });
            players.Add(new Player() { Name = "Serge Gnabry" });
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
