using System;
using System.Data.Entity;
using Mundialito.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL.Teams;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Games;
using Mundialito.DAL.Accounts;

namespace Mundialito.DAL
{
    public class MundialitoContextInitializer : DropCreateDatabaseIfModelChanges<MundialitoContext>
    {

        protected override void Seed(MundialitoContext context)
        {
            CreateAdminRoleAndUser(context);

            SetupDefaultData(context);

            base.Seed(context);
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

        private static void CreateAdminRoleAndUser(MundialitoContext context)
        {
            var UserManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create Role Admin if it does not exist
            string name = "Admin";
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            //Create User=Admin with password=123456
            var user = new MundialitoUser();
            user.UserName = "Admin";
            user.FirstName = Constants.AdminFirstName;
            user.LastName = Constants.AdminLastName;
            user.Email = Constants.AdminEmail;
            var adminresult = UserManager.Create(user, "123456");

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
        } 
    }
}