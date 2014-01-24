using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mundialito.Models
{
    public class Bet
    {
        public int BetId { get; set; }

        [Required]
        public virtual MundialitoUser User { get; set; }

        [Required]
        public virtual Game Game { get; set; }

        [Required]
        [Range(0, 10)]
        public int HomeScore { get; set; }

        [Required]
        [Range(0, 10)]
        public int AwayScore { get; set; }

        public String Mark
        {
            get
            {
                if (HomeScore == AwayScore) return "X";
                return HomeScore > AwayScore ? "1" : "2";
            }
        }
    }
}