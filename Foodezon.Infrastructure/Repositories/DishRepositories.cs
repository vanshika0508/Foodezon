using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext _context;

        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Dish>> GetAllAvailableAsync()
        {
            return await _context.Dishes
                .Include(d => d.Category)
                .Where(d => d.IsAvailable)
                .ToListAsync();
        }

        public  async Task<Dish?> GetByIdAsync(int id)
        {
            return await _context.Dishes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(d => d.Id == id && d.IsAvailable);
        }
    }
}