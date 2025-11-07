using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodezon.Core.Models
{
    // For adding a dish to the menu
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(600)]
        public string Details { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}