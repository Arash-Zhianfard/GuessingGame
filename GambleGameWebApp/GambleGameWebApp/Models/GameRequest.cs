using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class GameRequest
    {
        [Range(1, int.MaxValue)]
        [Required]
        public int Point { get; set; }
        [Range(0, 9)]
        [Required]
        public int Number { get; set; }
    }
}
