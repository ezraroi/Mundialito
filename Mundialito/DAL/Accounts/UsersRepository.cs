using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.DAL.Accounts
{
    public class UsersRepository : IUsersRepository
    {
        private MundialitoContext context;

        public UsersRepository(MundialitoContext context)
        {
            this.context = context;
        }

        public IEnumerable<MundialitoUser> AllUsers()
        {
            return context.Users;
        }

        public MundialitoUser GetUser(String username)
        {
            return context.Users.SingleOrDefault(user => user.UserName == username);
        }
    }
}