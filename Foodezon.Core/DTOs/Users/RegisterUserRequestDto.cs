
using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.DTOs.Users
{
    public class RegisterUserRequestDto
    {
        [Required]
        [MaxLength(70)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(70)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MinLength(16)]
        public string Password { get; set; } = string.Empty;
    }
}
