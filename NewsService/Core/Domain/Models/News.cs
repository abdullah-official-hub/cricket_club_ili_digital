using Framework.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewsService.Core.Domain.Models
{
    public class News : Entity
    {
        [Required]
        [StringLength(1500)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string NewsImageUrl { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
    }
}