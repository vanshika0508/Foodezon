using Foodezon.Core.DTOs.Dishes;


namespace Foodezon.Core.DTOs.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name{ get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        
        public List<DishDto> Dishes { get; set; } = new();
    }
}