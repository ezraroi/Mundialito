using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.DAL.ActionLogs
{
    public interface IActionLogsRepository
    {
        IEnumerable<ActionLog> GetAllLogs();

        ActionLog InsertLogAction(ActionLog logAction);

        void Save(); 
    }
}
