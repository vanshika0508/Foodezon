using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace Foodezon.Core.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public DateTime orderDate { get; set; } = DateTime.Now;
    }
}