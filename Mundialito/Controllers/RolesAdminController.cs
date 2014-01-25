using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL;
using Mundialito.DAL.Accounts;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace Mundialito.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesAdminController : ApiController
    {

        public UserManager<MundialitoUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        public MundialitoContext Context { get; private set; }

        public RolesAdminController()
        {
            Context = new MundialitoContext();
            UserManager = new UserManager<MundialitoUser>(new UserStore<MundialitoUser>(Context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Context));
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return Context.Roles.ToList();
        }

        public IdentityRole GetRole(string id)
        {
            var item = RoleManager.FindById(id);
            if (item == null)
                throw new ObjectNotFoundException(string.Format("Role with id '{0}' not found", id));
            return item; 
        }

        [HttpPost]
        public HttpResponseMessage PostRole(IdentityRole role)
        {
            var newRole = new IdentityRole(role.Name);
            var result = RoleManager.Create(newRole);
            if (!result.Succeeded)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(String.Join("\n", result.Errors));
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse<IdentityRole>(HttpStatusCode.Created, newRole);
        }

        public void PutRole(string id, IdentityRole role)
        {
            role.Id = id;
            var result = RoleManager.Update(role);
            if (!result.Succeeded)
                throw new Exception(String.Join("\n", result.Errors));
        }

        public void DeleteRole(string id)
        {
            var role = RoleManager.FindById(id);
            if (role == null)
                throw new ObjectNotFoundException(string.Format("Role with id '{0}' not found", id));
            Context.Roles.Remove(role);
            Context.SaveChanges();
        }
    }
}
