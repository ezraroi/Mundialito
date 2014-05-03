using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.DAL.Accounts
{
    public interface IUsersRepository : IDisposable
    {
        IEnumerable<MundialitoUser> AllUsers();

        MundialitoUser GetUser(String username);

        void DeleteUser(String username);

        void Save(); 
    }
}
