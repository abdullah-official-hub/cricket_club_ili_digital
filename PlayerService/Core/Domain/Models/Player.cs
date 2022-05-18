using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace PlayerService.Core.Domain.Models
{
    public class Player : Entity
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public short ShirtNumber { get; set; }
        public string ProfileImageUrl { get; set; }
        [Required]
        [StringLength(150)]
        public string Team { get; set; }
    }
}