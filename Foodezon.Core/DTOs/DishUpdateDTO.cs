using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.DTOs
{
    public class DishUpdateDTO
    {
        [Required]
        public int DishId { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}