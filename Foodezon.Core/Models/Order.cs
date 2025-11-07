using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace Foodezon.Core.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required, StringLength(100)]
        public string CustomerName { get; set; }

        [Required, StringLength(100)]
        public string CustomerEmail { get; set; }

        [Required, StringLength(50)]
        public string OrderStatus { get; set; } = "Pending";

        public DateTime orderDate { get; set; } = DateTime.Now;
    }
}