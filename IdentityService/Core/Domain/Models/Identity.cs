using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.Domain.Models
{
    public class Identity : Entity
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [StringLength(254)]
        public string Email { get; set; }
        [Required]
        [StringLength(150)]
        public string Password { get; set; }
    }
}