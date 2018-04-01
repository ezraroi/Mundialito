using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL.Teams;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Accounts;
using System.Web.Configuration;
using System.Collections.Generic;
using Mundialito.DAL.DBCreators;
using Mundialito.DAL.Bets;
using Mundialito.Logic;

namespace Mundialito.DAL
{
    public class MundialitoContextInitializer : DropCreateDatabaseIfModelChanges<MundialitoContext>
    {
        private Dictionary<String, Stadium> stadiumsDic = new Dictionary<string, Stadium>();
        private Dictionary<String, Team> teamsDic = new Dictionary<string, Team>();
        private UserManager<MundialitoUser> userManager;
        private bool monkeyEnabled = false;

        protected override void Seed(MundialitoContext context)
        {
            userManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(context));;

            CreateAdminRoleAndUsers(context);

            var creatorName = WebConfigurationManager.AppSettings["TournamentDBCreatorName"];

            if (!String.IsNullOrEmpty(creatorName))
            {
                Type t = Type.GetType("Mundialito.DAL.DBCreators." + creatorName);

                ITournamentCreator tournamentCreator = Activator.CreateInstance(t) as ITournamentCreator;

                SetupTeams(context, tournamentCreator);

                SetupStadiums(context, tournamentCreator);

                SetupGames(context, tournamentCreator);
            }
            base.Seed(context);
        }

        private void SetupTeams(MundialitoContext context, ITournamentCreator tournamentCreator)
        {
            var teams = tournamentCreator.GetTeams();

            teams.ForEach(team => context.Teams.Add(team));

            context.SaveChanges();
            teamsDic = context.Teams.ToDictionary(team => team.Name, team => team);
        }

        private void SetupStadiums(MundialitoContext context, ITournamentCreator tournamentCreator)
        {
            var stadiums = tournamentCreator.GetStadiums();

            stadiums.ForEach(stadium => context.Stadiums.Add(stadium));

            context.SaveChanges();
            stadiumsDic = context.Stadiums.ToDictionary(stadium => stadium.Name, stadium => stadium);
        }

        private void SetupGames(MundialitoContext context, ITournamentCreator tournamentCreator)
        {
            var games = tournamentCreator.GetGames(stadiumsDic, teamsDic);

            games.ForEach(stadium => context.Games.Add(stadium));

            context.SaveChanges();

            if (monkeyEnabled)
            {
                var monkey = userManager.FindByName(WebConfigurationManager.AppSettings["MonkeyUserName"]);

                var randomResults = new RandomResults();

                context.Games.ToList().ForEach(game =>
                {
                    var result = randomResults.GetRandomResult();
                    var newBet = new Bet();
                    newBet.UserId = monkey.Id;
                    newBet.GameId = game.GameId;
                    newBet.HomeScore = result.Key;
                    newBet.AwayScore = result.Value;
                    newBet.CardsMark = randomResults.GetRandomMark();
                    newBet.CornersMark = randomResults.GetRandomMark();
                    context.Bets.Add(newBet);
                });

                context.SaveChanges();
            }
        }

        private void CreateAdminRoleAndUsers(MundialitoContext context)
        {
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
            var adminresult = userManager.Create(user, "123456");

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, name);
            }

            monkeyEnabled = !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["MonkeyUserName"]);

            if (monkeyEnabled)
            {
                var monkey = new MundialitoUser();
                monkey.UserName = WebConfigurationManager.AppSettings["MonkeyUserName"];
                monkey.FirstName = "Monkey";
                monkey.LastName = "Monk";
                monkey.Email = "monkey@zoo.com";
                userManager.Create(monkey, "monkey");
            }
        } 
    }
}