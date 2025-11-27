namespace Foodezon.Core.DTOs
{
    public class DishDTO
    {
        public int DishId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}