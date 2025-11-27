using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Services
{
    public class DishAdminService : IDishAdminService
    {
        private readonly ApplicationDbContext _context;

        public DishAdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            return await _context.Dishes.Include(d => d.Category).ToListAsync();
        }

        public async Task<Dish?> GetByIdAsync(int id)
        {
            return await _context.Dishes.Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Dish> CreateAsync(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return dish;
        }

        public async Task UpdateAsync(Dish dish)
        {
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();
            }
        }
    }
}