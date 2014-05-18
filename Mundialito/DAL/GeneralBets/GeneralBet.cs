using Mundialito.DAL.Accounts;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public Team WinningTeam { get; set; }

        [Required]
        public String GoldBootPlayer { get; set; }

        public Boolean IsResolved { get; set; }

        public int? TeamPoints { get; set; }

        public int? PlayerPoints { get; set; }

        public Boolean IsOpen
        {
            get
            {
                return DateTime.Now.ToUniversalTime() < Constants.GeneralBetsCloseTime;
            }
        }
    }
}