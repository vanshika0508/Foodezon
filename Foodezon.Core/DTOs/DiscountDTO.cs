namespace Foodezon.Core.DTOs
{
    public class DiscountDTO
    {
        public int DiscountId { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Percentage { get; set; }
        public string DishName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}