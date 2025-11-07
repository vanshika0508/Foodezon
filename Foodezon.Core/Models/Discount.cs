using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodezon.Core.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [Required, StringLength(30)]
        public string DiscountCode { get; set; }

        [Required]
        [Range(0,100)]
        public decimal Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public int DishId { get; set; }

        public bool ActiveStatus => DateTime.Now >= StartDate && DateTime.Now <= EndDate;
    }
}