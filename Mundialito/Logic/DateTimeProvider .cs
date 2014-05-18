using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.Logic
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UTCNow { get { return DateTime.Now.ToUniversalTime(); } }
    }
}