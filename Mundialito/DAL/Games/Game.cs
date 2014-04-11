using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mundialito.DAL.Games
{
    public class Game 
    {
        public int GameId { get; set; }

        [Required]
        public Team HomeTeam { get; set; }

        [Required]
        public Team AwayTeam { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Range(0,10)]
        public int? HomeScore { get; set; }

        [Range(0, 10)]
        public int? AwayScore { get; set; }

        [Required]
        public Stadium Stadium { get; set; }

        public bool IsOpen 
        {
            get
            {
                var date = DateTime.Now.ToUniversalTime().Date;
                var time = DateTime.Now.ToUniversalTime().TimeOfDay;
                return (Date.Date > date) || (Date.Date == date && Date.TimeOfDay > time);
            }
        }

        public bool IsPendingUpdate
        {
            get
            {
                if (IsOpen) 
                    return false;
                return HomeScore == null && AwayScore == null;
            }
        }

        public bool IsBetResolved
        {
            get
            {
                return !IsOpen && !IsPendingUpdate;
            }
        }

        public String Mark
        {
            get
            {
                if (!IsOpen)
                {
                    if (IsPendingUpdate)
                        return "Pending Update";
                    if (HomeScore == AwayScore) return "X";
                    if (HomeScore > AwayScore) return "1";
                    return "2";
                }
                return "Not Played";
            }
        }

        public override string ToString()
        {
            return string.Format("Game ID = {0}, {1} - {2}", GameId, HomeTeam != null ? HomeTeam.Name : "Unknown", AwayTeam != null ? AwayTeam.Name : "Unknown");
        }
    }
}