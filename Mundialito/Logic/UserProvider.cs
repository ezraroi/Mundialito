using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;

namespace Mundialito.Logic
{
    public class UserProvider : IUserProvider
    {
        private readonly ApiController controller;
                public UserProvider(ApiController controller)
        {
            this.controller = controller;
        }

        public String UserId
        {
            get { return controller.User.Identity.GetUserId(); }
        }

        public String UserName
        {
            get { return controller.User.Identity.GetUserName(); }
        }
    }
}