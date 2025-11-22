using System.Collections.Generic;
using Foodezon.Core.DTOs.Categories;

namespace Foodezon.Api.Models.ViewModels
{
    public class MenuViewModel
    {
        public List<CategoryDto> Categories { get; set; } = new();
    }
}
