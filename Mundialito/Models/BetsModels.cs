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

        public BetViewModel(Bet bet)
        {
            BetId = bet.BetId;
            HomeScore = bet.HomeScore;
            AwayScore = bet.AwayScore;
            Game = new BetGame(bet.Game);
            User = new BetUser(bet.User);
        }

        public int BetId { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public BetUser User { get; set;}

        public BetGame Game { get; set; }

        public Boolean IsOpen { get; set; }
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

        public int BetId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        [Range(0,10)]
        public int HomeScore { get; set; }

        [Required]
        [Range(0, 10)]
        public int AwayScore { get; set; }

    }

    public class UpdateBetModel
    {
        public UpdateBetModel()
        {

        }

        [Required]
        [Range(0, 10)]
        public int HomeScore { get; set; }

        [Required]
        [Range(0, 10)]
        public int AwayScore { get; set; }
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
            IsOpen = game.IsOpen;
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