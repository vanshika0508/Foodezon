using System.Collections.Generic;
using Foodezon.Core.DTOs.Categories;
using Foodezon.Core.DTOs.Dishes;

namespace Foodezon.Api.Models.ViewModels
{
    public class HomeViewModel
    {
        public string Greeting { get; set; } = "Welcome to Foodezon";
        public string? SearchTerm { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
        public List<DishDto> SearchResults { get; set; } = new();
    }
}
