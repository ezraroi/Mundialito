using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.DAL.Accounts
{
    public class UsersRepository : GenericRepository<MundialitoUser>, IUsersRepository
    {
        public UsersRepository(MundialitoContext context)  : base(context)
        {
            
        }

        public IEnumerable<MundialitoUser> AllUsers()
        {
            return Context.Users;
        }

        public MundialitoUser GetUser(String username)
        {
            return Context.Users.SingleOrDefault(user => user.UserName == username);
        }


        public void DeleteUser(string id)
        {
            Delete(id);
        }

        
    }
}