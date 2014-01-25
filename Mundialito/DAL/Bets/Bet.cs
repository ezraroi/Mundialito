using Mundialito.DAL.Accounts;
using Mundialito.DAL.Games;
using Mundialito.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mundialito.DAL.Bets
{
    public class Bet
    {
        public Bet()
        {

        }

        public Bet(MundialitoUser user, Game game)
        {
            User = user;
            Game = game;
        }

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

        public Boolean IsOpen
        {
            get
            {
                return Game.IsOpen;
            }
        }

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