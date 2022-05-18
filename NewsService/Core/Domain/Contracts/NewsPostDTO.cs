using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewsService.Core.Domain.Contracts
{
    public class NewsPostDTO
    {
        [Required]
        [StringLength(1500)]
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile NewsImage { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
    }
}