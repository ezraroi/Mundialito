using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mundialito.Models
{
    public class Stadium
    {
        public int StadiumId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Capacity { get; set; }

        public List<Game> Games { get; set; }
    }
}