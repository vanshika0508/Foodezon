using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodezon.Core.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }

        public string DiscountCode { get; set; }

        public decimal Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DishId { get; set; }
    }
}