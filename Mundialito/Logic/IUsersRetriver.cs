using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Logic
{
    public interface IUsersRetriver
    {

        UserModel GetUser(String username, bool isLogged);
        List<UserModel> GetAllUsers();
    }
}
