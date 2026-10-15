using System.ComponentModel.DataAnnotations;

namespace RoyalVilla.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
    }
}
