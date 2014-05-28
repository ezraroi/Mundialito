using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.DAL.Games
{
    public static class GameExtensionMethods
    {
        public static Boolean IsOpen(this Game game)
        {
            return game.IsOpen(DateTime.UtcNow);
        }

        public static Boolean IsOpen(this Game game, DateTime now)
        {
            return now < game.CloseTime;
        }

        public static Boolean IsPendingUpdate(this Game game)
        {
            return game.IsPendingUpdate(DateTime.UtcNow);
        }

        public static Boolean IsPendingUpdate(this Game game, DateTime now)
        {
            if (game.IsOpen(now))
                return false;
            return game.HomeScore == null || game.AwayScore == null || game.CardsMark == null || game.CornersMark == null;
        }

        public static Boolean IsBetResolved(this Game game)
        {
            return game.IsBetResolved(DateTime.UtcNow);
        }

        public static Boolean IsBetResolved(this Game game, DateTime now)
        {
                return !game.IsOpen(now) && !game.IsPendingUpdate(now);
        }

        // TODO - Should be moved from here
        public static String Mark(this Game game)
        {
            return game.Mark(DateTime.UtcNow);
        }

        public static String Mark(this Game game, DateTime now)
        {
            if (!game.IsOpen(now))
            {
                if (game.IsPendingUpdate(now))
                    return "Pending Update";
                if (game.HomeScore == game.AwayScore) return "X";
                if (game.HomeScore > game.AwayScore) return "1";
                return "2";
            }
            return "Not Played";
        }
    }
}