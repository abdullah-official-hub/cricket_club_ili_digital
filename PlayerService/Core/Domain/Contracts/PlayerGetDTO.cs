using Microsoft.AspNetCore.Http;
using System;

namespace PlayerService.Core.Domain.Contracts
{
    public class PlayerGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short ShirtNumber { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Team { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}