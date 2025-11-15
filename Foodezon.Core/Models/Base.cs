using System.Collections.Generic;

namespace Foodezon.Core.Models
{
    public abstract class Base
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; }
    }
}