using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Logic
{
    public interface IBetsResolver
    {
        void ResolveBets(Game game);
    }
}
