using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
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
        private IUsersRetriver usersRetriver;
        private ILoggedUserProvider loggedUserProvider;
        private IUsersRepository usersRepository;
        private readonly UserManager<MundialitoUser> usersManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(new MundialitoContext()));

        public UsersController(IUsersRetriver usersRetriver, ILoggedUserProvider loggedUserProvider, IUsersRepository usersRepository)
        {
            this.usersRetriver = usersRetriver;
            this.loggedUserProvider = loggedUserProvider;
            this.usersRepository = usersRepository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var res = usersRetriver.GetAllUsers();
            res.ForEach(user => IsAdmin(user));
            return res.OrderByDescending(user => user.Points);
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
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public void DeleteUser(String id)
        {
            Trace.TraceInformation("Deleting user {0} by {1}", id, loggedUserProvider.UserName);
            usersRepository.DeleteUser(id);
            usersRepository.Save();
            // TODO - Should i delete all bets?
        }


        private void IsAdmin(UserModel user)
        {
            user.IsAdmin = usersManager.GetRoles<MundialitoUser>(user.Id).Contains("Admin");
        }

    }
}
