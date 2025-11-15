namespace Foodezon.Core.DTOs.Orders
{
    public class OrderItemDto
    {
        public int DishId { get; set; }
        public string DishName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
