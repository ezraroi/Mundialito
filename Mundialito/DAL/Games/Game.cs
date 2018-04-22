using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mundialito.DAL.Games
{
    public class Game 
    {
        public int GameId { get; set; }

        [Required]
        public int HomeTeamId { get; set; }
        
        public virtual Team HomeTeam { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Range(0,10)]
        public int? HomeScore { get; set; }

        [Range(0, 10)]
        public int? AwayScore { get; set; }

        public decimal RatioWeight { get; set; }
        public decimal HomeRatio { get; set; }
        public decimal TieRatio { get; set; }
        public decimal AwayRatio { get; set; }

        [StringLength(1)]
        [RegularExpression("[1X2-]")]
        public String CornersMark { get; set; }

        [StringLength(1)]
        [RegularExpression("[1X2-]")]
        public String CardsMark { get; set; }

        [Required]
        public int StadiumId { get; set; }
        
        public virtual Stadium Stadium { get; set; }

        public DateTime CloseTime
        {
            get
            {
                return Date.Subtract(TimeSpan.FromMinutes(30));
            }
        }
       
        public override string ToString()
        {
            return string.Format("Game ID = {0}, {1} - {2}", GameId, HomeTeam != null ? HomeTeam.Name : "Unknown", AwayTeam != null ? AwayTeam.Name : "Unknown");
        }
    }
}
