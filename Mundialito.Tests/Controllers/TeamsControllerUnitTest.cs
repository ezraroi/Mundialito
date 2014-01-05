using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mundialito.DAL.Teams;
using Moq;
using Mundialito.Controllers;
using Mundialito.Models;
using System.Collections.Generic;

namespace Mundialito.Tests.Controllers
{
    [TestClass]
    public class TeamsControllerUnitTest
    {

        [TestMethod]
        public void GetAllTeamsTest()
        {
            var teamsRepository = new Mock<ITeamsRepository>();
            var teams = new List<Team> { new Team(), new Team(), new Team() };

            teamsRepository.Setup(rep => rep.GetTeams()).Returns(teams);
            var controller = new TeamsController(teamsRepository.Object);
            Assert.AreEqual(3, controller.GetAllTeams().ToList().Count);
        }

        [TestMethod]
        public void GetTeamByIdTest()
        {
            var teamsRepository = new Mock<ITeamsRepository>();
            var teams = new List<Team>();
            teams.Add(new Team() { Name = "A", TeamId = 1 });
            teams.Add(new Team() { Name = "B", TeamId = 2 });

            teamsRepository.Setup(rep => rep.GetTeam(1)).Returns(teams.Where(team => team.TeamId == 1).Single());
            var controller = new TeamsController(teamsRepository.Object);
            Assert.AreEqual(1, controller.GetTeamById(1).TeamId);
            Assert.AreEqual("A", controller.GetTeamById(1).Name);
        }
    }
}
