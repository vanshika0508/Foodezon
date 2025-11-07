using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using foodezon.Core.Models;



namespace Foodezon.Core.Models
{
    public class Order : Base
    {

        public int UserId { get; set; }

        public int? DiscountId { get; set; }

        [Required]
        [StringLength(50)]

        public string OrderNumber { get; set; } = GenerateOrderNumber();

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Range(0, 1000)]
        public decimal Subtotal { get; set; }

        [Range(0, 200)]
        public decimal Discountamount { get; set; }

        [Range(0, 1000)]
        public decimal TotalAmount { get; set; }

        [StringLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        public virtual User User { get; set; } = null!;
        public virtual Discount? Discount { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        private static string GenerateOrderNumber()
        {
            return "ORD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
    public class OrderItem : Base
    {
        public int OrderId { get; set; }
        
        public int DishId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Order Order { get; set; } = null!;

        public virtual Dish Dish { get; set; } = null!;
        
        public decimal LineTotal => Quantity * UnitPrice;
    }

    public enum OrderStatus
    {
        Pending,
        confirmed,
        Preparing,
        ReadyForDelivery,
        OutForDelivery,
        Delivered,
        Cancelled
    }
}
