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
        public String UserId { get; set; }
                
        public MundialitoUser User { get; set; }

        [Required]
        public int GameId { get; set; }
        
        public Game Game { get; set; }

        [Required]
        [Range(0, 10)]
        public int HomeScore { get; set; }

        [Required]
        [Range(0, 10)]
        public int AwayScore { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("[1X2]")]
        public String CornersMark { get { return "X";  } set { CornersMark = value; } }

        [Required]
        [StringLength(1)]
        [RegularExpression("[1X2]")]
        public String CardsMark { get { return "X"; } set { CardsMark = value; } }

        public decimal? Points { get; set; }
        
        public Boolean CornersWin { get; set; }

        public Boolean GameMarkWin { get; set; }

        public Boolean ResultWin { get; set; }

        public Boolean CardsWin { get; set; }

        public override string ToString()
        {
            return string.Format("Bet ID = {0}, UserID = {1}, Game ID = {2}", BetId, UserId, GameId);
        }
    }
}