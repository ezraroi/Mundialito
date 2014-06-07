using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp
{
    public class TeamsEntityBackuper : AbstractEntityBackuper<Team>
    {
        public TeamsEntityBackuper(string directoryName)
            : base(directoryName, "Teams")
        {

        }

        protected override List<string> GetFieldsToBackup()
        {
            return new List<string>() { "TeamId", "Name", "Flag", "Logo", "ShortName" };
        }

        protected override List<Team> GetAllEntites()
        {
            var repository = new TeamsRepository();
            return repository.GetTeams().ToList();
        }
    }
}
