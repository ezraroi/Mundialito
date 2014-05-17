using Mundialito.DAL.Accounts;
using Mundialito.DAL.Games;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mundialito.DAL.Bets
{
    public interface IBetsRepository
    {
        IEnumerable<Bet> GetBets();

        IEnumerable<Bet> GetUserBets(string userId);

        IEnumerable<Bet> GetGameBets(int gameId);
        
        Bet GetBet(int betId);

        Bet InsertBet(Bet bet);

        void DeleteBet(int betId);

        void UpdateBet(Bet bet);

        void Save(); 
    }
}
