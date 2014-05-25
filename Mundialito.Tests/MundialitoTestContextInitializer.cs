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

            SetupDefaultData(context);

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
        }

        private static void SetupDefaultData(MundialitoContext context)
        {
            var eng = new Team { Name = "England", ShortName = "ENG", Flag = "http://www.fifa.com/imgml/flags/reflected/m/ENG.png", Logo = "http://www.fifa.com/imgml/logos/xs/ENG.gif" };
            var ita = new Team { Name = "Italy", ShortName = "ITA", Logo = "http://www.fifa.com/imgml/logos/xs/ITA.gif", Flag = "http://www.fifa.com/imgml/flags/reflected/m/ITA.png" };
            var uru = new Team { Name = "Uruguay", ShortName = "URU", Logo = "http://www.fifa.com/imgml/logos/xs/URU.gif", Flag = "http://www.fifa.com/imgml/flags/reflected/m/URU.png" };

            context.Teams.Add(eng);
            context.Teams.Add(ita);
            context.Teams.Add(uru);

            var stadium = new Stadium
            {
                Capacity = 15000,
                Name = "Blomfiled"
            };
            context.Stadiums.Add(stadium);

            context.Games.Add(new Game
            {
                HomeTeam = eng,
                AwayTeam = ita,
                HomeScore = 3,
                AwayScore = 0,
                Date = DateTime.Now,
                Stadium = new Stadium
                {
                    Capacity = 15000,
                    Name = "Kiryat Eliezer"
                }
            });

            context.Games.Add(new Game
            {
                HomeTeam = eng,
                AwayTeam = ita,
                HomeScore = 0,
                AwayScore = 0,
                Date = DateTime.Now.AddDays(2),
                Stadium = stadium
            });
        }

    }
}
