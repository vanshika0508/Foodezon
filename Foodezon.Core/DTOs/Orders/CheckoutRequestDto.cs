using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.DTOs.Orders
{
    public class CheckoutRequestDto
    {
        [Required]
        public int UserId { get; set; }

        
        public string? DiscountCode { get; set; }

       
        public string? DeliveryAddress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
