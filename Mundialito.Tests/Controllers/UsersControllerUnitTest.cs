using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mundialito.Controllers;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.ActionLogs;
using Mundialito.Logic;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Tests.Controllers
{
    [TestClass]
    public class UsersControllerUnitTest
    {
        [TestMethod]
        public void PlaceDiffTest()
        {
            var usersRetriver = new Mock<IUsersRetriver>();
            var users = new List<UserModel>();
            users.Add(new UserModel(new MundialitoUser() { Id = "1", UserName = "1" }) { Points = 10, YesterdayPoints = 3 });
            users.Add(new UserModel(new MundialitoUser() { Id = "2", UserName = "2" }) { Points = 20, YesterdayPoints = 1 });
            users.Add(new UserModel(new MundialitoUser() { Id = "3", UserName = "3" }) { Points = 30, YesterdayPoints = 2 });
            usersRetriver.Setup(item => item.GetAllUsers()).Returns(users);

            var loggedUserProvider = new Mock<ILoggedUserProvider>();
            loggedUserProvider.SetupGet(user => user.UserId).Returns("1");

            var usersRepository = new Mock<IUsersRepository>();

            var controller = CreateController(usersRetriver.Object, loggedUserProvider.Object, usersRepository.Object);
            var res = controller.GetAllUsers().ToDictionary(user => user.Id, user => user);
            Assert.AreEqual(res["1"].PlaceDiff, "-2");
            Assert.AreEqual(res["2"].PlaceDiff, "+1");
            Assert.AreEqual(res["3"].PlaceDiff, "+1");
            Assert.AreEqual(res["1"].Place, "3");
            Assert.AreEqual(res["2"].Place, "2");
            Assert.AreEqual(res["3"].Place, "1");
        }

        [TestMethod]
        public void PlaceDiffTest2()
        {
            var usersRetriver = new Mock<IUsersRetriver>();
            var users = new List<UserModel>();
            users.Add(new UserModel(new MundialitoUser() { Id = "1", UserName = "1" }) { Points = 30, YesterdayPoints = 3 });
            users.Add(new UserModel(new MundialitoUser() { Id = "2", UserName = "2" }) { Points = 10, YesterdayPoints = 1 });
            users.Add(new UserModel(new MundialitoUser() { Id = "3", UserName = "3" }) { Points = 20, YesterdayPoints = 2 });
            usersRetriver.Setup(item => item.GetAllUsers()).Returns(users);

            var loggedUserProvider = new Mock<ILoggedUserProvider>();
            loggedUserProvider.SetupGet(user => user.UserId).Returns("1");

            var usersRepository = new Mock<IUsersRepository>();

            var controller = CreateController(usersRetriver.Object, loggedUserProvider.Object, usersRepository.Object);
            var res = controller.GetAllUsers().ToDictionary(user => user.Id, user => user);
            Assert.AreEqual(res["1"].PlaceDiff, "0");
            Assert.AreEqual(res["2"].PlaceDiff, "0");
            Assert.AreEqual(res["3"].PlaceDiff, "0");
            Assert.AreEqual(res["1"].Place, "1");
            Assert.AreEqual(res["2"].Place, "3");
            Assert.AreEqual(res["3"].Place, "2");
        }


        private UsersController CreateController(IUsersRetriver usersRetriver, ILoggedUserProvider loggedUserProvider, IUsersRepository usersRepository)
        {
            var actionLogsRepository = new Mock<IActionLogsRepository>();
            var adminManagment = new Mock<IAdminManagment>();
            return new UsersController(usersRetriver, loggedUserProvider, usersRepository, adminManagment.Object, actionLogsRepository.Object);
        }
    }
}
