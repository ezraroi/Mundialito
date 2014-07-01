using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mundialito.Models
{
    public class GameViewModel
    {
        public GameViewModel(Game game)
        {
            GameId = game.GameId;
            HomeTeam = new GameTeamModel(game.HomeTeam);
            AwayTeam = new GameTeamModel(game.AwayTeam);
            Date = game.Date;
            HomeScore = game.HomeScore;
            AwayScore = game.AwayScore;
            CornersMark = game.CornersMark;
            CardsMark = game.CardsMark;
            Stadium = game.Stadium;
            IsOpen = game.IsOpen();
            IsPendingUpdate = game.IsPendingUpdate();
            IsBetResolved = game.IsBetResolved();
            Mark = game.Mark();
        }

        public int GameId { get; private set; }

        public GameTeamModel HomeTeam { get; private set; }

        public GameTeamModel AwayTeam { get; private set; }

        public DateTime Date { get; private set; }

        public int? HomeScore { get; private set; }

        public int? AwayScore { get; private set; }

        public String CornersMark { get; private set; }

        public String CardsMark { get; private set; }

        public Stadium Stadium { get; private set; }

        public Boolean UserHasBet { get; set; }

        public DateTime CloseTime
        {
            get
            {
                return Date.Subtract(TimeSpan.FromMinutes(30));
            }
        }
       
        public Boolean IsOpen  { get; private set; }
        
        public Boolean IsPendingUpdate { get; private set; }

        public Boolean IsBetResolved { get; private set; }

        public String Mark { get; private set; }

    }

    public class GameTeamModel
    {
        public GameTeamModel()
        {

        }

        public GameTeamModel(Team team)  
        {
            TeamId = team.TeamId;
            Name = team.Name;
            Flag = team.Flag;
            Logo = team.Logo;
            ShortName = team.ShortName;
        }

        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Flag { get; set; }

        public string Logo { get; set; }

        public string ShortName { get; set; }

    }

    public class NewGameModel
    {
        public int GameId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public Stadium Stadium { get; set; }

        [Required]
        public GameTeamModel HomeTeam { get; set; }

        [Required]
        public GameTeamModel AwayTeam { get; set; }

        public Boolean IsOpen { get; set; }

        public Boolean IsPendingUpdate { get; set; }
    }

    public class PutGameModel
    {
        public PutGameModel()
        {

        }

        public PutGameModel(Game game)
        {
            Date = game.Date;
            HomeScore = game.HomeScore;
            AwayScore = game.AwayScore;
            CornersMark = game.CornersMark;
            CardsMark = game.CardsMark;
        }

        public DateTime Date { get; set; }

        public int? HomeScore { get; set; }

        public int? AwayScore { get; set; }

        public String CornersMark { get; set; }

        public String CardsMark { get; set; }

    }

    public class PutGameModelResult : PutGameModel
    {
        public PutGameModelResult(Game game, DateTime now) : base(game)
        {
            GameId = game.GameId;
            IsOpen = game.IsOpen(now);
            IsPendingUpdate = game.IsPendingUpdate(now);
            IsBetResolved = game.IsBetResolved(now);
            Mark = game.Mark(now);
        }

        public int GameId { get; private set; }

        public Boolean IsOpen { get; private set; }

        public Boolean IsPendingUpdate { get; private set; }

        public Boolean IsBetResolved { get; private set; }

        public String Mark { get; private set; }

    }
}