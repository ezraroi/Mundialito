using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class AcceptenceTests
    {
        private TestContext testContextInstance;

        private BetsController betsController;
        private GamesController gamesController;
        private GeneralBetsController generalBetsController;
        private StadiumsController stadiumsController;
        private TeamsController teamsController;
        private UsersController usersController;


        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            DataBaseConnectionProvider.SetManualString(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=MundialitoTests;Integrated Security=True");
            Database.SetInitializer(new MundialitoTestContextInitializer());
            MundialitoContext c = new MundialitoContext();
            c.Database.Initialize(true);
        }

        [TestInitialize]
        public void CreateControllers()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.SetupGet(item => item.UTCNow).Returns(DateTime.UtcNow);
            var userProvider = new Mock<ILoggedUserProvider>();
            userProvider.SetupGet(user => user.UserId).Returns("Admin");
            betsController = new BetsController(new BetsRepository(), new BetValidator(new GamesRepository(), new BetsRepository()), userProvider.Object);
            gamesController = new GamesController(new GamesRepository(), new BetsRepository(), new BetsResolver(new BetsRepository()));
            generalBetsController = new GeneralBetsController(new GeneralBetsRepository(), userProvider.Object, dateTimeProvider.Object);
            stadiumsController = new StadiumsController(new StadiumsRepository());
            teamsController = new TeamsController(new TeamsRepository());
            usersController = new UsersController(new UsersRetriver(new BetsRepository(), new GeneralBetsRepository(), new UsersRepository()), userProvider.Object, new UsersRepository());
        }

        [TestMethod]
        public void Test1()
        {
            var res = teamsController.GetAllTeams();
            Assert.AreEqual(3, res.Count());
        }
    }
}
