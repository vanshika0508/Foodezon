using System.ComponentModel.DataAnnotations;

namespace Foodezon.Core.DTOs
{
    public class CategoryCreateDTO
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
    }
}