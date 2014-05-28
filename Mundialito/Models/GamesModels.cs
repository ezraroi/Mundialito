using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mundialito.Models
{
    public class GameViewModel
    {
        public GameViewModel(Game game)
        {
            GameId = game.GameId;
            HomeTeam = game.HomeTeam;
            AwayTeam = game.AwayTeam;
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

        public Team HomeTeam { get; private set; }

        public Team AwayTeam { get; private set; }

        public DateTime Date { get; private set; }

        public int? HomeScore { get; private set; }

        public int? AwayScore { get; private set; }

        public String CornersMark { get; private set; }

        public String CardsMark { get; private set; }

        public Stadium Stadium { get; private set; }

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
}