
using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.DTOs.Cart
{
    public class UpdateCartItemRequestDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int DishId { get; set; }

        [Range(0, 50)]
        public int Quantity { get; set; } 
    }
}
