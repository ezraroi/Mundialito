using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Tests
{
    public class MundialitoTestContextInitializer : DropCreateDatabaseAlways<MundialitoContext>
    {
        protected override void Seed(MundialitoContext context)
        {
            CreateAdminRoleAndUsers(context);

            SetupTestData(context);

            base.Seed(context);
        }

        private static void CreateAdminRoleAndUsers(MundialitoContext context)
        {
            var UserManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create Role Admin if it does not exist
            string name = "Admin";
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            var admin = new MundialitoUser();
            admin.UserName = "Admin";
            admin.FirstName = "Admin";
            admin.LastName = "Admin";
            admin.Email = "Admin@admin.com";
            var adminresult = UserManager.Create(admin, "123456");

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(admin.Id, name);
            }

            var user = new MundialitoUser();
            user.UserName = "User1";
            user.FirstName = "User1";
            user.LastName = "User1";
            user.Email = "User1@admin.com";
            UserManager.Create(user, "123456");

            var user2 = new MundialitoUser();
            user2.UserName = "User2";
            user2.FirstName = "User2";
            user2.LastName = "User2";
            user2.Email = "User2@admin.com";
            UserManager.Create(user2, "123456");

            var user3 = new MundialitoUser();
            user3.UserName = "User3";
            user3.FirstName = "User3";
            user3.LastName = "User3";
            user3.Email = "User3@admin.com";
            UserManager.Create(user3, "123456");
        }

        private static void SetupTestData(MundialitoContext context)
        {
            var teamA = new Team { Name = "TeamA", ShortName = "AAA", Flag = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png", Logo = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png" };
            var teamB = new Team { Name = "TeamB", ShortName = "BBB", Flag = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png", Logo = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png" };
            var teamC = new Team { Name = "TeamC", ShortName = "CCC", Flag = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png", Logo = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png" };
            var teamD = new Team { Name = "TeamD", ShortName = "DDD", Flag = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png", Logo = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png" };

            context.Teams.Add(teamA);
            context.Teams.Add(teamB);
            context.Teams.Add(teamC);
            context.Teams.Add(teamD);

            var stadiumA = new Stadium {Capacity = 15000, Name = "StadiumA", City = "CityA" };
            var stadiumB = new Stadium {Capacity = 15000, Name = "StadiumB", City = "CityB"  };

            context.Stadiums.Add(stadiumA);
            context.Stadiums.Add(stadiumB);

            context.Games.Add(new Game
            {
                HomeTeam = teamA,
                AwayTeam = teamB,
                Date = DateTime.UtcNow.AddDays(1),
                Stadium = stadiumA
            });

            context.Games.Add(new Game
            {
                HomeTeam = teamC,
                AwayTeam = teamD,
                Date = DateTime.UtcNow.AddDays(1),
                Stadium = stadiumB
            });

            context.Games.Add(new Game
            {
                HomeTeam = teamA,
                AwayTeam = teamC,
                Date = DateTime.UtcNow.AddDays(2),
                Stadium = stadiumA
            });

            context.Games.Add(new Game
            {
                HomeTeam = teamB,
                AwayTeam = teamD,
                Date = DateTime.UtcNow.AddDays(2),
                Stadium = stadiumB
            });

            context.Games.Add(new Game
            {
                HomeTeam = teamA,
                AwayTeam = teamD,
                Date = DateTime.UtcNow.AddDays(3),
                Stadium = stadiumA
            });

            context.Games.Add(new Game
            {
                HomeTeam = teamB,
                AwayTeam = teamC,
                Date = DateTime.UtcNow.AddDays(3),
                Stadium = stadiumB
            });
        }
    }
}