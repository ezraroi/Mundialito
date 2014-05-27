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

        [StringLength(1)]
        [RegularExpression("[1X2-]")]
        public String CornersMark { get; set; }

        [StringLength(1)]
        [RegularExpression("[1X2-]")]
        public String CardsMark { get; set; }

        [Required]
        public Stadium Stadium { get; set; }

        public DateTime CloseTime
        {
            get
            {
                return Date.Subtract(TimeSpan.FromMinutes(30));
            }
        }

        public bool IsOpen 
        {
            get
            {
                return DateTime.UtcNow < CloseTime;
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