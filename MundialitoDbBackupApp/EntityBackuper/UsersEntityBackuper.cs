using Mundialito.DAL.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp
{
    class UsersEntityBackuper : AbstractEntityBackuper<MundialitoUser>
    {
        public UsersEntityBackuper(string directoryName)
            : base(directoryName, "Users")
        {

        }

        protected override List<string> GetFieldsToBackup()
        {
            return new List<string>() { "FirstName", "LastName", "Email", "Id", "UserName" };
        }

        protected override List<MundialitoUser> GetAllEntites()
        {
            var repository = new UsersRepository();
            return repository.AllUsers().ToList();
        }
    }
}
