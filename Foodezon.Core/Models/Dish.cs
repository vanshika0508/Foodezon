using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.Models
{
    // For adding a dish to the menu
    public class Dish
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}