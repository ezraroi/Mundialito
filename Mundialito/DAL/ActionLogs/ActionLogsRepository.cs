using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.DAL.ActionLogs
{
    public class ActionLogsRepository : GenericRepository<ActionLog>, IActionLogsRepository
    {
        public ActionLogsRepository()
            : base(new MundialitoContext())
        {
            
        }

        public IEnumerable<ActionLog> GetAllLogs()
        {
            return Get();
        }

        public ActionLog InsertLogAction(ActionLog logAction)
        {
            return Insert(logAction);
        }
    }
}