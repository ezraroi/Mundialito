using Mundialito.DAL.ActionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundialitoDbBackupApp.EntityBackuper
{
    class ActionLogsEntityBackuper : AbstractEntityBackuper<ActionLog>
    {
        public ActionLogsEntityBackuper(string directoryName)
            : base(directoryName, "Action Logs")
        {

        }

        protected override List<string> GetFieldsToBackup()
        {
            return new List<string>() { "ActionLogId", "Username", "Type", "ObjectType", "Timestamp", "Message"};
        }

        protected override List<ActionLog> GetAllEntites()
        {
            var repository = new ActionLogsRepository();
            return repository.GetAllLogs().ToList();
        }

        protected override object ProcessValue(object obj, string propName)
        {
            if (propName == "Type")
                return Enum.GetName(typeof(ActionType), obj);
            return obj;
        }
    }
}
