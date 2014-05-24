﻿using Mundialito.DAL.Accounts;
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
            return Context.Bets.Include(bet => bet.User).Include(bet => bet.Game).Include(bet => bet.User).Include(bet => bet.Game).Include(bet => bet.Game.AwayTeam).Include(bet => bet.Game.HomeTeam);
        }

        public IEnumerable<Bet> GetUserBets(string username)
        {
            return Context.Bets.Include(bet => bet.User).Include(bet => bet.Game).Include(bet => bet.User).Include(bet => bet.Game).Include(bet => bet.Game.AwayTeam).Include(bet => bet.Game.HomeTeam).Where(bet => bet.User.UserName == username);
        }

        public IEnumerable<Bet> GetGameBets(int gameId)
        {
            return Context.Bets.Include(bet => bet.User).Include(bet => bet.Game).Include(bet => bet.Game.AwayTeam).Include(bet => bet.Game.HomeTeam).Where(bet => bet.Game.GameId == gameId);
        }

        public Bet GetBet(int betId)
        {
            return GetByID(betId);
        }

        public Bet InsertBet(Bet bet)
        {
            // TODO - Check the status of the attched items, force that the items are not new
            Context.Games.Attach(bet.Game);
            Context.Users.Attach(bet.User);
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