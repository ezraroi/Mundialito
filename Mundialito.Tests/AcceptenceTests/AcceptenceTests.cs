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

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            AcceptenceTestsUtils.InitDatabase();
        }

        /*
        [TestInitialize]
        public void CreateControllers()
        {
           
        }
        */

        [TestMethod]
        public void Test1()
        {
            var res = AcceptenceTestsUtils.GetTeamsController().GetAllTeams();
            Assert.AreEqual(3, res.Count());
        }

        [TestMethod]
        public void Test2()
        {
            var res = AcceptenceTestsUtils.GetTeamsController().GetAllTeams();
            Assert.AreEqual(3, res.Count());
        }
    }
}
