using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Mundialito.Logic
{
    public class BetsResolver : IBetsResolver
    {
        private readonly IBetsRepository betsRepository;

        public BetsResolver(IBetsRepository betsRepository)
        {
            this.betsRepository = betsRepository;

        }

        public void ResolveBets(Game game)
        {
            if (!game.IsBetResolved)
                throw new ArgumentException(string.Format("Game {0} is not resolved yet", game.GameId));
            var bets = betsRepository.GetGameBets(game.GameId);
            foreach (Bet bet in bets)
            {
                var points = 0;
                if (bet.Mark == game.Mark)
                    points += 3;
                if ((bet.HomeScore == game.HomeScore) && (bet.AwayScore == game.AwayScore))
                    points +=2 ;
                if (game.CardsMark == bet.CardsMark)
                {
                    points += 1;
                    bet.CardsWin = true;
                }
                if (game.CornersMark == bet.CornersMark)
                {
                    points += 1;
                    bet.CornersWin = true;
                }
                bet.Points = points;
                
                betsRepository.UpdateBet(bet);
                Trace.TraceInformation("{0} of {1} got {2} points", bet, game, points);
            }
            betsRepository.Save();
        }
    }
}