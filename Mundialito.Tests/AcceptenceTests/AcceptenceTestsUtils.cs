using Moq;
using Mundialito.Controllers;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.GeneralBets;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using Mundialito.Logic;
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

        public static BetsController GetBetsControllerAsAdmin()
        {
            return GetBetsController("Admin");
        }

        public static BetsController GetBetsController(String username)
        {
            return new BetsController(new BetsRepository(), new BetValidator(new GamesRepository(), new BetsRepository()), GetLoggedUserProvider(username));
        }

        public static GamesController GetGamesController()
        {
            return new GamesController(new GamesRepository(), new BetsRepository(), new BetsResolver(new BetsRepository()));
        }

        public static StadiumsController GetStadiumsController()
        {
            return new StadiumsController(new StadiumsRepository());
        }

        public static TeamsController GetTeamsController()
        {
            return new TeamsController(new TeamsRepository());
        }

        public static UsersController GetUsersControllerAsAdmin()
        {
            return GetUsersController("Admin");
        }

        public static UsersController GetUsersController(String username)
        {
            return new UsersController(new UsersRetriver(new BetsRepository(), new GeneralBetsRepository(), new UsersRepository()), GetLoggedUserProvider(username), new UsersRepository());
        }

        public static GeneralBetsController GetGeneralBetsControllerAsAdmin(DateTime now)
        {
            return GetGeneralBetsController("Admin", now);
        }

        public static GeneralBetsController GetGeneralBetsController(String username, DateTime now)
        {
            return new GeneralBetsController(new GeneralBetsRepository(), GetLoggedUserProvider(username), GeDateTimeProvider(now));
        }

        private static ILoggedUserProvider GetLoggedUserProvider(String username)
        {
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(user => user.UserId).Returns(username);
            return userProvider.Object;
        }

        private static IDateTimeProvider GeDateTimeProvider(DateTime utcNow)
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.SetupGet(item => item.UTCNow).Returns(utcNow);
            return dateTimeProvider.Object;
        }
    }
}