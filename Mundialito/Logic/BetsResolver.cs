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
                var points = 0;
                if (bet.Mark() == game.Mark(dateTimeProvider.UTCNow))
                {
                    points += 3;
                    bet.GameMarkWin = true;
                }
                else
                    bet.GameMarkWin = false;
                if ((bet.HomeScore == game.HomeScore) && (bet.AwayScore == game.AwayScore))
                {
                    points += 2;
                    bet.ResultWin = true;
                }
                else
                    bet.ResultWin = false;
                if (game.CardsMark == bet.CardsMark)
                {
                    points += 1;
                    bet.CardsWin = true;
                }
                else
                    bet.CardsWin = false;
                if (game.CornersMark == bet.CornersMark)
                {
                    points += 1;
                    bet.CornersWin = true;
                }
                else
                    bet.CornersWin = false;
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