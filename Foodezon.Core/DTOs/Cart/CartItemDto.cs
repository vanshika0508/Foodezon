
namespace Foodezon.Core.DTOs.Cart
{
    public class CartItemDto
    {
        public int DishId      { get; set; }
        public string DishName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity      { get; set; }
        public decimal LineTotal { get; set; }
        public string ImageUrl   { get; set; } = string.Empty;
    }
}
