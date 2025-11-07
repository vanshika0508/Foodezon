using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.Models
{
    public class Cart : Base
    {
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal TotalPrice
        {
            get
            {
                return CartItems.Sum(item => item.Quantity*item.Dish.Price);
            }
        }
    }

    public class CartItem : Base
    {
        public int CartId { get; set; }
        public int DishId { get; set; }
        [Range(1, 50)]
        public int Quantity { get; set; } = 1;

        public virtual Cart Cart { get; set; } = null!;

        public virtual Dish Dish { get; set; } = null!;

        public decimal LineTotal => Quantity * Dish.Price;

    }
}