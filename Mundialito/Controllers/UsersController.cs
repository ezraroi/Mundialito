using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.ActionLogs;
using Mundialito.Logic;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Users")]
    [Authorize]
    public class UsersController : ApiController
    {
        private const String ObjectType = "User";
        private readonly IUsersRetriver usersRetriver;
        private readonly ILoggedUserProvider loggedUserProvider;
        private readonly IUsersRepository usersRepository;
        private readonly IActionLogsRepository actionLogsRepository;
        private readonly IAdminManagment adminManagment;

        public UsersController(IUsersRetriver usersRetriver, ILoggedUserProvider loggedUserProvider, IUsersRepository usersRepository, IAdminManagment adminManagment, IActionLogsRepository actionLogsRepository)
        {
            this.usersRetriver = usersRetriver;
            this.loggedUserProvider = loggedUserProvider;
            this.usersRepository = usersRepository;
            this.actionLogsRepository = actionLogsRepository;
            this.adminManagment = adminManagment;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var res = GetTableDetails().ToList();
            res.ForEach(user => IsAdmin(user));
            return res;
        }

        [Route("table")]
        [HttpGet]
        public IEnumerable<UserModel> GetTable()
        {
            return GetTableDetails();
        }

        [Route("{username}")]
        public UserModel GetUserByUsername(String username)
        {
            var res = usersRetriver.GetUser(username, loggedUserProvider.UserName == username);
            IsAdmin(res);
            return res;
        }

        [Route("me")]
        public UserModel GetMe()
        {
            return GetUserByUsername(loggedUserProvider.UserName);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GeneratePrivateKey/{email}")]
        public HttpResponseMessage GeneratePrivateKey(string email)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                
                Content = new StringContent(PrivateKeyValidator.GeneratePrivateKey(email), Encoding.UTF8, "text/plain")
            };
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("MakeAdmin/{id}")]
        public void MakeAdmin(String id)
        {
            adminManagment.MakeAdmin(id);
            AddLog(ActionType.UPDATE, string.Format("Made  user {0} admin", id));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public void DeleteUser(String id)
        {
            Trace.TraceInformation("Deleting user {0} by {1}", id, loggedUserProvider.UserName);
            usersRepository.DeleteUser(id);
            usersRepository.Save();
            AddLog(ActionType.DELETE, string.Format("Deleted user: {0}", id));
        }

        private void IsAdmin(UserModel user)
        {
            user.IsAdmin = adminManagment.IsAdmin(user.Id);
        }

        private void AddLog(ActionType actionType, String message)
        {
            try
            {
                actionLogsRepository.InsertLogAction(ActionLog.Create(actionType, ObjectType, message));
                actionLogsRepository.Save();
            }
            catch (Exception e)
            {
                Trace.TraceError("Exception during log. Exception: {0}", e.Message);
            }
        }

        private IEnumerable<UserModel> GetTableDetails()
        {
            var res = usersRetriver.GetAllUsers();
            var yesterdayPlaces = new Dictionary<string, int>(res.Count);
            res = res.OrderByDescending(user => user.YesterdayPoints).ToList();
            for (int i = 0; i < res.Count; i++)
            {
                yesterdayPlaces.Add(res[i].Id, i + 1);
            }
            res = res.OrderByDescending(user => user.Points).ToList();
            for (int i = 0; i < res.Count; i++)
            {
                res[i].Place = (i + 1).ToString();
                var diff = yesterdayPlaces[res[i].Id] - (i + 1);
                res[i].PlaceDiff = string.Format("{0}{1}", diff > 0 ? "+" : string.Empty, diff);
                res[i].TotalMarks = res[i].Marks + res[i].Results;
            }
            return res;
        }
    }
}
