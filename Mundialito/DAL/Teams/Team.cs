using Mundialito.DAL.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mundialito.DAL.Teams
{
    public class Team
    {
        public int TeamId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Flag { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string ShortName { get; set; }

        public ICollection<Game> HomeMatches { get; set; }

        public ICollection<Game> AwayMatches { get; set; }
    }
}