using System;
using System.ComponentModel.DataAnnotations;

namespace Mundialito.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [Required]
        public Team HomeTeam { get; set; }

        [Required]
        public Team AwayTeam { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Range(0,10)]
        public int HomeScore { get; set; }

        [Range(0, 10)]
        public int AwayScore { get; set; }

        [Required]
        public Stadium Stadium { get; set; }

        public bool IsOpen 
        {
            get
            {
                var date = DateTime.Now.Date;
                var time = DateTime.Now.TimeOfDay;
                return (Date > date) || (Date == date && Time > time);
            }
        }

        public String Mark
        {
            get
            {
                if (!IsOpen)
                {
                    if (HomeScore == AwayScore) return "X";
                    if (HomeScore > AwayScore) return "1";
                    return "2";
                }
                return "Not Played";
            }
        }
    }
}