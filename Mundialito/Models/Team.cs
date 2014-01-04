using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mundialito.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}