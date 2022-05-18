using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewsService.Core.Domain.Contracts
{
    public class NewsPutDTO
    {
        [StringLength(1500)]
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ProfileImage { get; set; }
        public DateTime PublishDate { get; set; }
    }
}