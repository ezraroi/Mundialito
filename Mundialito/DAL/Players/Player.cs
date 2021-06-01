using System.ComponentModel.DataAnnotations;

namespace Mundialito.DAL.Players
{
    public class Player
    {
        public int PlayerId { get; set; }
        
        [Required]
        public string Name { get; set; }

    }
}