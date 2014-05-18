using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Logic
{
    public interface IDateTimeProvider
    {
        DateTime UTCNow { get; }
    }
}
