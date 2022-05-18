using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PlayerService.Core.Domain.Contracts
{
    public class PlayerPostDTO
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public short ShirtNumber { get; set; }
        public IFormFile ProfileImage { get; set; }
        [Required]
        [StringLength(150)]
        public string Team { get; set; }
    }
}