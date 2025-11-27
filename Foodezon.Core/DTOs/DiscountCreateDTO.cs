using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.DTOs
{
    public class DiscountCreateDTO
    {
        [Required, StringLength(50)]
        public string Code { get; set; } = string.Empty;
        [Required, Range(0, 100)]
        public decimal Percentage { get; set; }
        [Required]
        public int DishId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}