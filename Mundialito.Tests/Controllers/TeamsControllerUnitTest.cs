using System.Linq;
using Mundialito.DAL.Teams;
using Moq;
using Mundialito.Controllers;
using System.Collections.Generic;
using Mundialito.DAL.ActionLogs;
using NUnit.Framework;

namespace Mundialito.Tests.Controllers
{
    [TestFixture]
    public class TeamsControllerUnitTest
    {

        [Test]
        public void GetAllTeamsTest()
        {
            var teamsRepository = new Mock<ITeamsRepository>();
            var teams = new List<Team> { new Team(), new Team(), new Team() };

            teamsRepository.Setup(rep => rep.GetTeams()).Returns(teams);
            var controller = CreateController(teamsRepository.Object);
            Assert.AreEqual(3, controller.GetAllTeams().ToList().Count);
        }

        [Test]
        public void GetTeamByIdTest()
        {
            var teamsRepository = new Mock<ITeamsRepository>();
            var teams = new List<Team>();
            teams.Add(new Team() { Name = "A", TeamId = 1 });
            teams.Add(new Team() { Name = "B", TeamId = 2 });

            teamsRepository.Setup(rep => rep.GetTeam(1)).Returns(teams.Where(team => team.TeamId == 1).Single());
            var controller = CreateController(teamsRepository.Object);
            Assert.AreEqual(1, controller.GetTeamById(1).TeamId);
            Assert.AreEqual("A", controller.GetTeamById(1).Name);
        }

        private TeamsController CreateController(ITeamsRepository repository)
        {
            var actionLogsRepository = new Mock<IActionLogsRepository>();
            return new TeamsController(repository, actionLogsRepository.Object);
        }
    }
}
