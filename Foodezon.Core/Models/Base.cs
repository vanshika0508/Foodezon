using System.Collections.Generic;

namespace Foodezon.Core.Models
{
    public abstract class Base
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}