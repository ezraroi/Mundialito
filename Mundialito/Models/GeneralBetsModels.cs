using Mundialito.DAL.GeneralBets;
using System;

namespace Mundialito.Models
{

    public class GeneralBetViewModel
    {
        public GeneralBetViewModel(GeneralBet bet)
        {
            GeneralBetId = bet.GeneralBetId;
            WinningTeamId = bet.WinningTeamId;
            GoldenBootPlayerId = bet.GoldBootPlayerId;
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

        public int GoldenBootPlayerId { get; set; }

        public Boolean IsResolved { get; set; }

        public Boolean IsClosed { get; private set; }
       
        public int Points { get; set; }

        public DateTime CloseTime { get; set; }
    }

    public class NewGeneralBetModel
    {
        public int WinningTeamId { get; set; }

        public int GoldenBootPlayerId { get; set; }

        public int GeneralBetId { get; set; }
    }

    public class UpdateGenralBetModel
    {
        public int WinningTeamId { get; set; }

        public int GoldenBootPlayerId { get; set; }
    }

    public class ResolveGeneralBetModel
    {
        public Boolean PlayerIsRight { get; set; }

        public Boolean TeamIsRight { get; set; }
    }
}