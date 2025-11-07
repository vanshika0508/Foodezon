using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodezon.Core.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int DishId { get; set; }

        public int Quantity { get; set; }
    }
}