using Mundialito.DAL.Accounts;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mundialito.Models
{

    public class BetViewModel
    {
        public BetViewModel()
        {

        }

        public BetViewModel(Bet bet, DateTime now)
        {
            BetId = bet.BetId;
            HomeScore = bet.HomeScore;
            AwayScore = bet.AwayScore;
            CornersWin = bet.CornersWin;
            CardsWin = bet.CardsWin;
            CornersMark = bet.CornersMark;
            CardsMark = bet.CardsMark;
            GameMarkWin = bet.GameMarkWin;
            ResultWin = bet.ResultWin;
            IsOpenForBetting = bet.IsOpenForBetting(now);
            IsResolved = bet.IsResolved(now);
            Points = bet.Points.HasValue ? bet.Points.Value : 0;
            Game = new BetGame(bet.Game);
            User = new BetUser(bet.User);
        }

        public int BetId { get; set; }

        public int? HomeScore { get; set; }

        public int? AwayScore { get; set; }

        public String CornersMark { get; set; }

        public String CardsMark { get; set; }

        public Boolean CornersWin { get; set; }

        public Boolean CardsWin{ get; set; }

        public Boolean GameMarkWin { get; set; }

        public Boolean ResultWin { get; set; }

        public int Points { get; set; }

        public BetUser User { get; set;}

        public BetGame Game { get; set; }

        public Boolean IsOpenForBetting { get; set; }

        public Boolean IsResolved { get; set; }
    }

    public class BetUser
    {
        public BetUser()
        {

        }
        public BetUser(MundialitoUser mundialitoUser)
        {
            UserName = mundialitoUser.UserName;
            FirstName = mundialitoUser.FirstName;
            LastName = mundialitoUser.LastName;
        }
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class NewBetModel
    {
        public NewBetModel()
        {

        }

        public NewBetModel(int id, UpdateBetModel bet)
        {
            BetId = id;
            GameId = bet.GameId;
            HomeScore = bet.HomeScore;
            AwayScore = bet.AwayScore;
            CornersMark = bet.CornersMark;
            CardsMark = bet.CardsMark;
        }

        public int BetId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        [Range(0,10)]
        public int HomeScore { get; set; }

        [Required]
        [Range(0, 10)]
        public int AwayScore { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("[1X2]")]
        public String CornersMark { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("[1X2]")]
        public String CardsMark { get; set; }
    }

    public class UpdateBetModel
    {
        public UpdateBetModel()
        {

        }

        [Required]
        public int GameId { get; set; }

        [Required]
        [Range(0, 10)]
        public int HomeScore { get; set; }

        [Required]
        [Range(0, 10)]
        public int AwayScore { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("[1X2]")]
        public String CornersMark { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("[1X2]")]
        public String CardsMark { get; set; }
    }
    
    public class BetGame
    {
        public BetGame()
        {

        }

        public BetGame(Game game)
        {
            GameId = game.GameId;
            HomeTeam = new BetGameTeam(game.HomeTeam);
            AwayTeam = new BetGameTeam(game.AwayTeam);
            IsOpen = game.IsOpen();
        }
        public int GameId { get; set; }

        public BetGameTeam HomeTeam { get; set; }

        public BetGameTeam AwayTeam { get; set; }

        public Boolean IsOpen { get; set; }

    }

    public class BetGameTeam
    {

        public BetGameTeam()
        {

        }

        public BetGameTeam(Team team)
        {
            TeamId = team.TeamId;
            Name = team.Name;
            ShortName = team.ShortName;
        }
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}