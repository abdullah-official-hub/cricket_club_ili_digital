using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.Domain.Contracts
{
    public class LoginDTO
    {
        [Required]
        [StringLength(254)]
        public string Email { get; set; }
        [Required]
        [StringLength(150)]
        public string Password { get; set; }
    }
}