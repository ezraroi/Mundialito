using Moq;
using Mundialito.Controllers;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.ActionLogs;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.GeneralBets;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using Mundialito.Logic;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Tests.AcceptenceTests
{
    public class AcceptenceTestsUtils
    {
        public static void InitDatabase()
        {
            DataBaseConnectionProvider.SetManualString(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=MundialitoTests;Integrated Security=True");
            Database.SetInitializer(new MundialitoTestContextInitializer());
            MundialitoContext c = new MundialitoContext();
            c.Database.Initialize(true);
        }

        public static BetsController GetBetsController(UserModel user, DateTime now)
        {
            return new BetsController(new BetsRepository(), new BetValidator(new GamesRepository(), new BetsRepository(), GetDateTimeProvider(now), new ActionLogsRepository()), GetLoggedUserProvider(user), GetDateTimeProvider(now), new ActionLogsRepository());
        }

        public static GamesController GetGamesController(UserModel user, DateTime now)
        {
            return new GamesController(new GamesRepository(), new BetsRepository(), new BetsResolver(new BetsRepository(), GetDateTimeProvider(now), new ActionLogsRepository()), GetLoggedUserProvider(user), GetDateTimeProvider(now), new ActionLogsRepository());
        }

        public static StadiumsController GetStadiumsController()
        {
            return new StadiumsController(new StadiumsRepository(), new ActionLogsRepository());
        }

        public static TeamsController GetTeamsController()
        {
            return new TeamsController(new TeamsRepository(), new ActionLogsRepository());
        }

        public static UsersController GetUsersController(UserModel user, DateTime now)
        {
            return new UsersController(new UsersRetriver(new BetsRepository(), new GeneralBetsRepository(), new UsersRepository(), GetDateTimeProvider(now)), GetLoggedUserProvider(user), new UsersRepository(), new AdminManagment(), new ActionLogsRepository());
        }

        public static GeneralBetsController GetGeneralBetsController(UserModel user, DateTime now)
        {
            return new GeneralBetsController(new GeneralBetsRepository(), GetLoggedUserProvider(user), GetDateTimeProvider(now), new ActionLogsRepository());
        }

        private static ILoggedUserProvider GetLoggedUserProvider(UserModel user)
        {
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(item => item.UserId).Returns(user.Id);
            userProvider.SetupGet(item => item.UserName).Returns(user.Username);
            return userProvider.Object;
        }

        private static IDateTimeProvider GetDateTimeProvider(DateTime utcNow)
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.SetupGet(item => item.UTCNow).Returns(utcNow);
            return dateTimeProvider.Object;
        }
    }
}