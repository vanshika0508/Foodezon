using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required]
        [ MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [ MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;
    }
}
