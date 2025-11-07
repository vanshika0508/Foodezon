using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Foodezon.Core.Models;

namespace foodezon.Core.Models
{
    public class Discount : Base
    {
        public string code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Percentage { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}