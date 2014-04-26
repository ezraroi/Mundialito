using Mundialito.DAL.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Logic
{
    public interface ILoggedUserProvider
    {
        String UserId { get; }

        String UserName { get; }
    }
}
