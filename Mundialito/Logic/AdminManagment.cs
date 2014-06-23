using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.Logic
{
    public class AdminManagment : IAdminManagment
    {
        private readonly UserManager<MundialitoUser> usersManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(new MundialitoContext()));

        public void MakeAdmin(string userId)
        {
            usersManager.AddToRole<MundialitoUser>(userId, "Admin");
        }

        public bool IsAdmin(string userId)
        {
            return usersManager.GetRoles<MundialitoUser>(userId).Contains("Admin");
        }
    }
}