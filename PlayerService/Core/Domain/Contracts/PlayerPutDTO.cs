using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PlayerService.Core.Domain.Contracts
{
    public class PlayerPutDTO
    {
        [StringLength(150)]
        public string Name { get; set; }
        public short ShirtNumber { get; set; }
        public IFormFile ProfileImage { get; set; }
        [StringLength(150)]
        public string Team { get; set; }
    }
}