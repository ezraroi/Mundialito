using Mundialito.DAL.ActionLogs;
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
        private const String ObjectType = "Bet";
        private readonly IBetsRepository betsRepository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IActionLogsRepository actionLogsRepository;

        public BetsResolver(IBetsRepository betsRepository, IDateTimeProvider dateTimeProvider, IActionLogsRepository actionLogsRepository)
        {
            this.betsRepository = betsRepository;
            this.dateTimeProvider = dateTimeProvider;
            this.actionLogsRepository = actionLogsRepository;
        }

        public void ResolveBets(Game game)
        {
            if (!game.IsBetResolved(dateTimeProvider.UTCNow))
                throw new ArgumentException(string.Format("Game {0} is not resolved yet", game.GameId));

            var bets = betsRepository.GetGameBets(game.GameId);
            foreach (Bet bet in bets)
            {
                bet.CornersWin = false;
                bet.CardsWin = false;
                bet.ResultWin = (bet.HomeScore == game.HomeScore) && (bet.AwayScore == game.AwayScore);
                var points = 0.0m;
                if (bet.Mark() == game.Mark(dateTimeProvider.UTCNow))
                {
                    if (bet.HomeScore > bet.AwayScore) 
                    {
                        points += game.HomeRatio;   
                    }
                    if (bet.HomeScore == bet.AwayScore)
                    {
                        points += game.TieRatio;
                    }
                    if (bet.HomeScore > bet.AwayScore)
                    {
                        points += game.AwayRatio;
                    }
                    points *= game.RatioWeight;
                    if (bet.ResultWin)
                    {
                        points *= 1.3m;
                    }
                    bet.GameMarkWin = true;
                }
                else
                {
                    bet.GameMarkWin = false;
                }

                bet.Points = points;
                betsRepository.UpdateBet(bet);
                Trace.TraceInformation("{0} of {1} got {2} points", bet, game, points);
                AddLog(ActionType.UPDATE, String.Format("Resolved bet {0} with points {1}", bet, points));
            }
            if (bets.Count() > 0)
                betsRepository.Save();
        }

        private void AddLog(ActionType actionType, String message)
        {
            try
            {
                actionLogsRepository.InsertLogAction(ActionLog.Create(actionType, ObjectType, message));
                actionLogsRepository.Save();
            }
            catch (Exception e)
            {
                Trace.TraceError("Exception during log. Exception: {0}", e.Message);
            }
        }
    }
}
