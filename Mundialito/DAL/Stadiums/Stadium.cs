using Mundialito.DAL.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mundialito.DAL.Stadiums
{
    public class Stadium
    {
        public int StadiumId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Capacity { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}