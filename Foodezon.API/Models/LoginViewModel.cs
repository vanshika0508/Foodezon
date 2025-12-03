using System.ComponentModel.DataAnnotations;

namespace Foodezon.Api.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]   
        public string Password { get; set; }
    }
}