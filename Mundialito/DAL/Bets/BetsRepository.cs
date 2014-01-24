using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mundialito.DAL.Bets
{
    public class BetsRepository : GenericRepository<Bet>, IBetsRepository
    {

        public BetsRepository()
            : base(new MundialitoContext())
        {
        }

        public IEnumerable<Bet> GetBets()
        {
            return Context.Bets.Include(bet => bet.User).Include(bet => bet.Game);
        }

        public IEnumerable<Bet> GetUserBets(string userId)
        {
            return Context.Bets.Where(bet => bet.User.Id == userId).Include(bet => bet.User).Include(bet => bet.Game);
        }

        public IEnumerable<Bet> GetGameBets(int gameId)
        {
            return Context.Bets.Where(bet => bet.Game.GameId == gameId).Include(bet => bet.User).Include(bet => bet.Game);
        }

        public Bet GetBet(int betId)
        {
            return GetByID(betId);
        }

        public Bet InsertBet(Bet bet)
        {
            return Insert(bet);
        }

        public void DeleteBet(int betId)
        {
            Delete(betId);
        }

        public void UpdateBet(Bet bet)
        {
            Update(bet);
        }
    }
}