using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foodezon.Core.Entities;


namespace Foodezon.Core.Models
{
    public class Dish : Base
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [Range(0.01,1000)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}