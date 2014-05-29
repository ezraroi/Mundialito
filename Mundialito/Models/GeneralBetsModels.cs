using Mundialito.DAL.GeneralBets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Mundialito.Models
{

    public class GeneralBetViewModel
    {
        public GeneralBetViewModel(GeneralBet bet)
        {
            GeneralBetId = bet.GeneralBetId;
            WinningTeamId = bet.WinningTeamId;
            GoldenBootPlayer = bet.GoldBootPlayer;
            IsResolved = bet.IsResolved;
            if (IsResolved)
                Points = bet.PlayerPoints.Value + bet.TeamPoints.Value;
            CloseTime = TournamentTimesUtils.GeneralBetsCloseTime;
            OwnerName = string.Format("{0} {1}", bet.User.FirstName, bet.User.LastName);
            IsClosed = DateTime.UtcNow > CloseTime;
        }

        public int GeneralBetId { get; set; }

        public int WinningTeamId { get; set; }

        public String OwnerName { get; private set; }

        public String GoldenBootPlayer { get; set; }

        public Boolean IsResolved { get; set; }

        public Boolean IsClosed { get; private set; }
       
        public int Points { get; set; }

        public DateTime CloseTime { get; set; }
    }

    public class NewGeneralBetModel
    {
        public int WinningTeamId { get; set; }

        public string GoldenBootPlayer { get; set; }

        public int GeneralBetId { get; set; }
    }

    public class UpdateGenralBetModel
    {
        public int WinningTeamId { get; set; }

        public string GoldenBootPlayer { get; set; }
    }

    public class ResolveGeneralBetModel
    {
        public Boolean PlayerIsRight { get; set; }

        public Boolean TeamIsRight { get; set; }
    }
}