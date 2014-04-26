using Mundialito.Logic;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mundialito.Controllers
{
    [RoutePrefix("api/Users")]
    [Authorize]
    public class UsersController : ApiController
    {
        private IUsersRetriver usersRetriver;
        private ILoggedUserProvider loggedUserProvider;

        public UsersController(IUsersRetriver usersRetriver, ILoggedUserProvider loggedUserProvider)
        {
            this.usersRetriver = usersRetriver;
            this.loggedUserProvider = loggedUserProvider;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return usersRetriver.GetAllUsers();
        }

        [Route("{username}")]
        public UserModel GetUserByUsername(String username)
        {
            return usersRetriver.GetUser(username, loggedUserProvider.UserName == username);
        }

        [Route("me")]
        public UserModel GetMe()
        {
            return GetUserByUsername(loggedUserProvider.UserName);
        }

    }
}
