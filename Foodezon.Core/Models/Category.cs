using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodezon.Core.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        
        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Details { get; set; }
    }
}