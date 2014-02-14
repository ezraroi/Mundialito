using Microsoft.AspNet.Identity;
using Mundialito.DAL.Bets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.Logic
{
    public interface IBetValidator
    {
        void ValidateNewBet(Bet bet);
        void ValidateUpdateBet(Bet bet);
        void ValidateDeleteBet(int betId, string userId);
    }
}
