using System;
using System.Data.Entity;
using System.Linq;
using Mundialito.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL.Teams;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Games;
using Mundialito.DAL.Accounts;
using System.Web.Configuration;
using System.Configuration;
using System.Collections.Generic;

namespace Mundialito.DAL
{
    public class MundialitoContextInitializer : DropCreateDatabaseAlways<MundialitoContext>
    {
        private Dictionary<String, Stadium> stadiumsDic = new Dictionary<string, Stadium>();
        private Dictionary<String, Team> teamsDic = new Dictionary<string, Team>();

        protected override void Seed(MundialitoContext context)
        {
            CreateAdminRoleAndUser(context);

            SetupBrazil2014Data(context);

            base.Seed(context);
        }

        private void SetupBrazil2014Data(MundialitoContext context)
        {
            SetupTeams(context);

            SetupStadiums(context);

            SetupGames(context);

            
        }

        private void SetupGames(MundialitoContext context)
        {
            context.Games.Add(new Game
            {
                HomeTeamId = GetTeam("BRAZIL").TeamId,
                AwayTeamId = GetTeam("CROATIA").TeamId,
                Date = new DateTime(2014,5,30,20,0,0),
                //Date = new DateTime(2014,6,12,23,0,0),
                StadiumId = GetStadium("Arena de Sao Paulo").StadiumId
            });

            context.Games.Add(new Game
            {
                HomeTeamId = GetTeam("MEXICO").TeamId,
                AwayTeamId = GetTeam("CAMEROON").TeamId,
                Date = new DateTime(2014, 5, 31, 16, 0, 0),
                //Date = new DateTime(2014, 6, 13, 19, 0, 0),
                StadiumId = GetStadium("Estadio das Dunas").StadiumId
            });

            context.Games.Add(new Game
            {
                HomeTeamId = GetTeam("SPAIN").TeamId,
                AwayTeamId = GetTeam("NETHERLANDS").TeamId,
                Date = new DateTime(2014, 5, 31, 19, 0, 0),
                //Date = new DateTime(2014, 6, 13, 22, 0, 0),
                StadiumId = GetStadium("Arena Fonte Nova").StadiumId
            });

            context.Games.Add(new Game
            {
                HomeTeamId = GetTeam("CHILE").TeamId,
                AwayTeamId = GetTeam("AUSTRALIA").TeamId,
                Date = new DateTime(2014, 5, 31, 22, 0, 0),
                //Date = new DateTime(2014, 6, 14, 1, 0, 0),
                StadiumId = GetStadium("Arena Pantanal").StadiumId
            });
        }

        private void SetupTeams(MundialitoContext context)
        {
            var teams = new List<Team>();
            teams.Add(CreateTeam("USA", "USA"));
            teams.Add(CreateTeam("MEXICO", "MEX"));
            teams.Add(CreateTeam("HONDURAS", "HON"));
            teams.Add(CreateTeam("COSTA RICA", "CRC"));
            teams.Add(CreateTeam("COLOMBIA", "COL"));
            teams.Add(CreateTeam("ECUADOR", "ECU"));
            teams.Add(CreateTeam("BRAZIL", "BRA"));
            teams.Add(CreateTeam("CHILE", "CHI"));
            teams.Add(CreateTeam("URUGUAY", "URU"));
            teams.Add(CreateTeam("ARGENTINA", "ARG"));
            teams.Add(CreateTeam("JAPAN", "JPN"));
            teams.Add(CreateTeam("KOREA REPUBLIC", "KOR"));
            teams.Add(CreateTeam("AUSTRALIA", "AUS"));
            teams.Add(CreateTeam("IRAN", "IRN"));
            teams.Add(CreateTeam("ALGERIA", "ALG"));
            teams.Add(CreateTeam("GHANA", "GHA"));
            teams.Add(CreateTeam("CÔTE D'IVOIRE", "CIV"));
            teams.Add(CreateTeam("CAMEROON", "CMR"));
            teams.Add(CreateTeam("NIGERIA", "NGA"));
            teams.Add(CreateTeam("RUSSIA", "RUS"));
            teams.Add(CreateTeam("ENGLAND", "ENG"));
            teams.Add(CreateTeam("NETHERLANDS", "NED"));
            teams.Add(CreateTeam("BELGIUM", "BEL"));
            teams.Add(CreateTeam("GERMANY", "GER"));
            teams.Add(CreateTeam("FRANCE", "FRA"));
            teams.Add(CreateTeam("SWITZERLAND", "SUI"));
            teams.Add(CreateTeam("CROATIA", "CRO"));
            teams.Add(CreateTeam("BOSNIA AND HERZEGOVINA", "BIH"));
            teams.Add(CreateTeam("SPAIN", "ESP"));
            teams.Add(CreateTeam("PORTUGAL", "POR"));
            teams.Add(CreateTeam("ITALY", "ITA"));
            teams.Add(CreateTeam("GREECE", "GRE"));

            teams.ForEach(team => context.Teams.Add(team));

            context.SaveChanges();
            teamsDic = context.Teams.ToDictionary(team => team.Name, team => team);
        }

        private Team GetTeam(String teamName)
        {
            return teamsDic[teamName];
        }

        private Stadium GetStadium(String name)
        {
            return stadiumsDic[name];
        }

        private Team CreateTeam(String name, String shortName)
        {
            return new Team() { Name = name, ShortName = shortName, Flag = string.Format("http://www.fifa.com/imgml/flags/reflected/m/{0}.png", shortName), Logo = string.Format("http://www.fifa.com/imgml/logos/xs/{0}.gif", shortName) };
        }

        private void SetupStadiums(MundialitoContext context)
        {
            var stadiums = new List<Stadium>();
            stadiums.Add(new Stadium() { Name = "Arena Amazonia", Capacity = 39118, City = "Manaus" });
            stadiums.Add(new Stadium() { Name = "Arena da Baixada", Capacity = 38533, City = "Curitiba" });
            stadiums.Add(new Stadium() { Name = "Arena de Sao Paulo", Capacity = 61606, City = "Sao Paulo" });
            stadiums.Add(new Stadium() { Name = "Arena Fonte Nova", Capacity = 51708, City = "Salvador" });
            stadiums.Add(new Stadium() { Name = "Arena Pantanal", Capacity = 39859, City = "Cuiaba" });
            stadiums.Add(new Stadium() { Name = "Arena Pernambuco", Capacity = 42583, City = "Recife" });
            stadiums.Add(new Stadium() { Name = "Estadio Beira-Rio", Capacity = 42991, City = "Porto Alegre" });
            stadiums.Add(new Stadium() { Name = "Estadio Castelao", Capacity = 60348, City = "Fortaleza" });
            stadiums.Add(new Stadium() { Name = "Estadio das Dunas", Capacity = 38958, City = "Natal" });
            stadiums.Add(new Stadium() { Name = "Estadio Mineirao", Capacity = 58259, City = "Belo Horizonte" });
            stadiums.Add(new Stadium() { Name = "Estadio Nacional de Brasilia", Capacity = 69432, City = "Brasilia" });
            stadiums.Add(new Stadium() { Name = "Estadio Do Maracana", Capacity = 74689, City = "Rio de Janeiro" });

            stadiums.ForEach(stadium => context.Stadiums.Add(stadium));

            context.SaveChanges();
            stadiumsDic = context.Stadiums.ToDictionary(stadium => stadium.Name, stadium => stadium);
        }

        private void CreateAdminRoleAndUser(MundialitoContext context)
        {
            var UserManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create Role Admin if it does not exist
            string name = "Admin";
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            //Create Admin user with password=123456
            var user = new MundialitoUser();
            user.UserName = WebConfigurationManager.AppSettings["AdminUserName"];
            user.FirstName = WebConfigurationManager.AppSettings["AdminFirstName"];
            user.LastName = WebConfigurationManager.AppSettings["AdminLastName"];
            user.Email = WebConfigurationManager.AppSettings["AdminEmail"];
            var adminresult = UserManager.Create(user, "123456");

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
        } 
    }
}