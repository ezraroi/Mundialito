using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Logic
{
    public interface IAdminManagment
    {
        void MakeAdmin(string userId);

        bool IsAdmin(string userId);
    }
}
