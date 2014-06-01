using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;

namespace Mundialito.DAL.ActionLogs
{
    public class ActionLog
    {
        public ActionLog()
        {

        }

        public int ActionLogId { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        public ActionType Type { get; set; }

        [Required]
        public String ObjectType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        [Required]
        public String Message { get; set; }

        public static ActionLog Create(ActionType logType, String objectType, String message)
        {
            return new ActionLog() 
            {
                Message = message,
                ObjectType = objectType,
                Username = Thread.CurrentPrincipal.Identity.GetUserName(),
                Type = logType,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}