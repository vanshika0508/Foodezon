using System.ComponentModel.DataAnnotations;

namespace Foodezon.Api.Models
{
    public class DiscountFormViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Code { get; set; } = string.Empty;

        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
        public decimal Percentage { get; set; }
        public DateTime ValidFrom { get; set; } = DateTime.UtcNow;
        public DateTime ValidTo { get; set; } = DateTime.UtcNow.AddDays(30);
        public bool IsActive { get; set; } = true;
    }
}