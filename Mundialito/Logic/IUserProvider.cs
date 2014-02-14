using Mundialito.DAL.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Logic
{
    public interface IUserProvider
    {
        String UserId { get; }

        String UserName { get; }
    }
}
