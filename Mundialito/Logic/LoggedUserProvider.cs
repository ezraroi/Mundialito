using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Mundialito.Logic
{
    public class LoggedUserProvider : ILoggedUserProvider
    {
        public LoggedUserProvider()
        {

        }

        public String UserId
        {
            get { return Thread.CurrentPrincipal.Identity.GetUserId(); }
        }

        public String UserName
        {
            get { return Thread.CurrentPrincipal.Identity.GetUserName(); }
        }
    }
}