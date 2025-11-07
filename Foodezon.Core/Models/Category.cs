using System.Collections.Generic;
using Foodezon.Core.Models;

namespace Foodezon.Core.Entities
{
    public class Category : Base
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
        


    }
}