
using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.DTOs.Cart
{
    public class AddToCartRequestDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int DishId { get; set; }

        [Range(1, 50)]
        public int Quantity { get; set; } = 1;
    }
}
