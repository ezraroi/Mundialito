using System;
using System.Data.Entity;
using Mundialito.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            var haifa = new Team { Name = "Maccabi Haifa", ShortName = "HFA", Flag = "http://www.fifa.com/imgml/flags/reflected/m/POR.png", Logo = "http://www.fifa.com/imgml/logos/xs/POR.gif"};
            var mTelAviv = new Team { Name = "Maccabi Tel Aviv", ShortName = "MTV" , Logo = "http://www.fifa.com/imgml/logos/xs/POR.gif" , Flag = "http://www.fifa.com/imgml/flags/reflected/m/POR.png"};
            var hapoel = new Team { Name = "Hapoel Tel Aviv", ShortName = "HTV", Logo = "http://www.fifa.com/imgml/logos/xs/POR.gif", Flag = "http://www.fifa.com/imgml/flags/reflected/m/POR.png" };

            context.Teams.Add(haifa);
            context.Teams.Add(mTelAviv);
            context.Teams.Add(hapoel);

            var stadium = new Stadium
            {
                Capacity = 15000,
                Name = "Blomfiled"
            };
            context.Stadium.Add(stadium);

            context.Games.Add(new Game
            {
                HomeTeam = haifa,
                AwayTeam = mTelAviv,
                HomeScore = 3,
                AwayScore = 0,
                Time = DateTime.Now.TimeOfDay,
                Date = DateTime.Now.Date,
                Stadium = new Stadium
                {
                    Capacity = 15000,
                    Name = "Kiryat Eliezer"
                }
            });

            context.Games.Add(new Game
            {
                HomeTeam = haifa,
                AwayTeam = mTelAviv,
                HomeScore = 0,
                AwayScore = 0,
                Time = DateTime.Now.TimeOfDay,
                Date = DateTime.Now.Date.AddDays(2),
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
            user.FirstName = "Mundialito";
            user.LastName = "Admin";
            user.Email = "ezraroi@gmail.com";
            var adminresult = UserManager.Create(user, "123456");

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
        } 
    }
}