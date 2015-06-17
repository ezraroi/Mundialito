using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.DAL.DBCreators
{
    public class Copa2015TournamentCreator : ITournamentCreator
    {
        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            teams.Add(CreateTeam("BOLIVIA", "BOL"));
            teams.Add(CreateTeam("JAMAICA", "JAM"));
            teams.Add(CreateTeam("PARAGUAY", "PAR"));
            teams.Add(CreateTeam("PERU", "PER"));
            teams.Add(CreateTeam("VENEZUELA", "VEN"));
            teams.Add(CreateTeam("MEXICO", "MEX"));
            teams.Add(CreateTeam("COLOMBIA", "COL"));
            teams.Add(CreateTeam("ECUADOR", "ECU"));
            teams.Add(CreateTeam("BRAZIL", "BRA"));
            teams.Add(CreateTeam("CHILE", "CHI"));
            teams.Add(CreateTeam("URUGUAY", "URU"));
            teams.Add(CreateTeam("ARGENTINA", "ARG"));
            return teams;
        }

        public List<Game> GetGames(Dictionary<String, Stadium> stadiums, Dictionary<String, Team> teams)
        {
            var games = new List<Game>();
            games.Add(new Game
            {
                HomeTeamId = teams["CHILE"].TeamId,
                AwayTeamId = teams["ECUADOR"].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 11, 23, 30, 0)),
                StadiumId = stadiums["Estadio Nacional Julio Martínez Prádanos"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["MEXICO"].TeamId,
                AwayTeamId = teams["BOLIVIA"].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 12, 23, 30, 0)),
                StadiumId = stadiums["Estadio Sausalito"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["URUGUAY"].TeamId,
                AwayTeamId = teams["JAMAICA"].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 13, 19, 0, 0)),
                StadiumId = stadiums["Estadio Regional Calvo y Bascuñán"].StadiumId
            });            

            games.Add(new Game
            {
                HomeTeamId = teams["ARGENTINA"].TeamId,
                AwayTeamId = teams["PARAGUAY"].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 13, 21, 30, 0)),
                StadiumId = stadiums["Estadio La Portada de La Serena"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["COLOMBIA"].TeamId,
                AwayTeamId = teams["VENEZUELA"].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 14, 19, 0, 0)),
                StadiumId = stadiums["Estadio El Teniente"].StadiumId
            });           

            games.Add(new Game
            {
                HomeTeamId = teams["BRAZIL"].TeamId,
                AwayTeamId = teams["PERU"].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 14, 21, 30, 0)),
                StadiumId = stadiums["Estadio Municipal Bicentenario Germán Becker"].StadiumId
            });

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["ECUADOR"].TeamId,
                AwayTeamId = teams["BOLIVIA"].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 15, 21, 0, 0)),
                StadiumId = stadiums["Estadio Elías Figueroa Brander"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["CHILE"].TeamId,
                AwayTeamId = teams["MEXICO"].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 15, 23, 30, 0)),
                StadiumId = stadiums["Estadio Nacional Julio Martínez Prádanos"].StadiumId
            });          

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["Paraguay".ToUpper()].TeamId,
                AwayTeamId = teams["Jamaica".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 16, 21, 0, 0)),
                StadiumId = stadiums["Estadio Regional Calvo y Bascuñán"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["Argentina".ToUpper()].TeamId,
                AwayTeamId = teams["Uruguay".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 16, 23, 30, 0)),
                StadiumId = stadiums["Estadio Regional Calvo y Bascuñán"].StadiumId
            });
            
            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["Brazil".ToUpper()].TeamId,
                AwayTeamId = teams["Colombia".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 18, 00, 0, 0)),
                StadiumId = stadiums["Estadio Monumental David Arellano"].StadiumId
            });            

            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["Peru".ToUpper()].TeamId,
                AwayTeamId = teams["Venezuela".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 18, 23, 30, 0)),
                StadiumId = stadiums["Estadio Elías Figueroa Brander"].StadiumId
            });
           
            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["Mexico".ToUpper()].TeamId,
                AwayTeamId = teams["Ecuador".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 19, 21, 0, 0)),
                StadiumId = stadiums["Estadio El Teniente"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["Chile".ToUpper()].TeamId,
                AwayTeamId = teams["Bolivia".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 19, 23, 30, 0)),
                StadiumId = stadiums["Estadio Nacional Julio Martínez Prádanos"].StadiumId
            });
            
            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["Uruguay".ToUpper()].TeamId,
                AwayTeamId = teams["Paraguay".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 20, 19, 0, 0)),
                StadiumId = stadiums["Estadio La Portada de La Serena"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["Argentina".ToUpper()].TeamId,
                AwayTeamId = teams["Jamaica".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 20, 21, 30, 0)),
                StadiumId = stadiums["Estadio Sausalito"].StadiumId
            });
            
            /* */

            games.Add(new Game
            {
                HomeTeamId = teams["Colombia".ToUpper()].TeamId,
                AwayTeamId = teams["Peru".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 21, 19, 0, 0)),
                StadiumId = stadiums["Estadio Municipal Bicentenario Germán Becker"].StadiumId
            });

            games.Add(new Game
            {
                HomeTeamId = teams["Brazil".ToUpper()].TeamId,
                AwayTeamId = teams["Venezuela".ToUpper()].TeamId,
                Date = GetFixedDate(new DateTime(2015, 6, 21, 21, 0, 0)),
                StadiumId = stadiums["Estadio Monumental David Arellano"].StadiumId
            });            

            return games;
        }

        public List<Stadium> GetStadiums()
        {
            var stadiums = new List<Stadium>();
            stadiums.Add(new Stadium() { Name = "Estadio Nacional Julio Martínez Prádanos", Capacity = 48665, City = "Santiago de Chile" });
            stadiums.Add(new Stadium() { Name = "Estadio Sausalito", Capacity = 22340, City = "Vina del Mar" });
            stadiums.Add(new Stadium() { Name = "Estadio Regional Calvo y Bascuñán", Capacity = 21178, City = "Antofagasta" });
            stadiums.Add(new Stadium() { Name = "Estadio La Portada de La Serena", Capacity = 17194, City = "La Serena" });
            stadiums.Add(new Stadium() { Name = "Estadio El Teniente", Capacity = 14087, City = "Rancagua" });
            stadiums.Add(new Stadium() { Name = "Estadio Municipal Bicentenario Germán Becker", Capacity = 18936, City = "Temuco" });
            stadiums.Add(new Stadium() { Name = "Estadio Elías Figueroa Brander", Capacity = 20575, City = "Valparaiso" });
            stadiums.Add(new Stadium() { Name = "Estadio Monumental David Arellano", Capacity = 47347, City = "Santiago de Chile" });
            stadiums.Add(new Stadium() { Name = "Estadio Municipal Alcaldesa Ester Roa Rebolledo", Capacity = 35000, City = "Concepcion" });

            return stadiums;
        }

        private DateTime GetFixedDate(DateTime date)
        {
            return date;
            //return date.AddDays(65);
        }

        private Team CreateTeam(String name, String shortName)
        {
            return new Team() { Name = name, ShortName = shortName, Flag = string.Format("http://www.fifa.com/imgml/flags/reflected/m/{0}.png", shortName), Logo = string.Format("http://www.fifa.com/imgml/logos/xs/{0}.gif", shortName) };
        }
    }
}
