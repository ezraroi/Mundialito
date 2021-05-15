using Mundialito.DAL.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mundialito.DAL.Players
{
    public class Player
    {
        public int PlayerId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public int TeamId { get; set; }

    }
}