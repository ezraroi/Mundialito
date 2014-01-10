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
    }
}