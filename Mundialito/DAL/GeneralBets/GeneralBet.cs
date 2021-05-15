using Mundialito.DAL.Accounts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mundialito.DAL.GeneralBets
{
    public class GeneralBet
    {

        public GeneralBet()
        {

        }

        public void Resolve(Boolean player, Boolean team)
        {
            IsResolved = true;
            TeamPoints = team ? 12 : 0;
            PlayerPoints = player ? 12 : 0;
        }

        public int GeneralBetId { get; set; }

        [Required]
        public MundialitoUser User { get; set; }

        [Required]
        public int WinningTeamId { get; set; }

        [Required]
        public int GoldBootPlayerId { get; set; }

        public Boolean IsResolved { get; set; }

        public int? TeamPoints { get; set; }

        public int? PlayerPoints { get; set; }

        public Boolean IsOpen
        {
            get
            {
                return DateTime.Now.ToUniversalTime() < TournamentTimesUtils.GeneralBetsCloseTime;
            }
        }

        public override string ToString()
        {
            return string.Format("General Bet ID = {0}, Owner = {1}, WinningTeamId = {2}, GoldBootPlayerId = {3}, TeamPoints = {4}, PlayerPoints = {5}", GeneralBetId, User == null ? "Unkown" : User.UserName, WinningTeamId, GoldBootPlayerId, TeamPoints.HasValue ? TeamPoints.Value.ToString() : "NA", PlayerPoints.HasValue ? PlayerPoints.Value.ToString() : "NA");
        }
    }
}