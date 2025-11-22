namespace Foodezon.Api.Models.ViewModels
{
    public class CartItemViewModel
    {
        public int DishId { get; set; }
        public string DishName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
    }
}
