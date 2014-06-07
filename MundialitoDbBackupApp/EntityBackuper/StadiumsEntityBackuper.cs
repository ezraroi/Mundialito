using Mundialito.DAL.Stadiums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp.EntityBackuper
{
    public class StadiumsEntityBackuper : AbstractEntityBackuper<Stadium>
    {
        public StadiumsEntityBackuper(string directoryName)
            : base(directoryName, "Stadiums")
        {

        }

        protected override List<string> GetFieldsToBackup()
        {
            return new List<string>() { "StadiumId", "Name", "City", "Capacity"};
        }

        protected override List<Stadium> GetAllEntites()
        {
            var repository = new StadiumsRepository();
            return repository.GetStadiums().ToList();
        }
    }
}
