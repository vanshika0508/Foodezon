
using Foodezon.Core.DTOs.Categories;
using Foodezon.Core.DTOs.Dishes;
using Foodezon.Core.Interfaces;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.Dishes)
                .ToListAsync();

            return categories.Select(MapToDto);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Dishes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return null;

            return MapToDto(category);
        }

        private CategoryDto MapToDto(Core.Models.Category category)
        {
            var dishDtos = category.Dishes
                .Where(d => d.IsAvailable)
                .Select(d => new DishDto
                {
                    Id           = d.Id,
                    Name         = d.Name,
                    Description  = d.Description,
                    Price        = d.Price,
                    ImageUrl     = d.ImageUrl,
                    IsAvailable  = d.IsAvailable,
                    CategoryName = category.Name
                })
                .ToList();

            return new CategoryDto
            {
                Id          = category.Id,
                Name        = category.Name,
                Description = category.Description,
                Dishes      = dishDtos
            };
        }
    }
}
