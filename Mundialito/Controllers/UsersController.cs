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
        private readonly UserManager<MundialitoUser> usersManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(new MundialitoContext()));

        public UsersController(IUsersRetriver usersRetriver, ILoggedUserProvider loggedUserProvider, IUsersRepository usersRepository, IActionLogsRepository actionLogsRepository)
        {
            this.usersRetriver = usersRetriver;
            this.loggedUserProvider = loggedUserProvider;
            this.usersRepository = usersRepository;
            this.actionLogsRepository = actionLogsRepository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var res = usersRetriver.GetAllUsers();
            res.ForEach(user => IsAdmin(user));
            res = res.OrderByDescending(user => user.Points).ToList();
            for (int i = 0; i < res.Count; i++)
            {
                res[i].Place = (i + 1).ToString();
            }
            return res;
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
                Content = new StringContent(PrivateKeyValidator.GeneratePrivateKey(email), Encoding.UTF8, "application/json")
            };
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("MakeAdmin/{id}")]
        public void MakeAdmin(String id)
        {
            usersManager.AddToRole<MundialitoUser>(id, "Admin");
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
            user.IsAdmin = usersManager.GetRoles<MundialitoUser>(user.Id).Contains("Admin");
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
    }
}
