using System.ComponentModel.DataAnnotations;

namespace Foodezon.Api.Models.ViewModels
{
    public class CheckoutViewModel
    {
        // Customer info
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Delivery Address")]
        public string Address { get; set; } = string.Empty;

        // Discount
        [Display(Name = "Discount Code")]
        public string? DiscountCode { get; set; }

        // Summary
        public CartViewModel Cart { get; set; } = new();

        // After placing order
        public bool OrderPlaced { get; set; }
        public string? OrderNumber { get; set; }
        public decimal? FinalTotal { get; set; }
    }
}
