using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly FoodezonDbContext _context;

        public DishRepository(FoodezonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync()
        {
            return await _context.Dishes
            .Include(d => d.Category)
            .ToListAsync();
        }

        public async Task<Dish> GetByIdAsync(int id)
        {
            return await _context.Dishes
            .Include(_d => _d.Category)
            .FirstOrDefaultAsync(d => d.DishId == id);
        }

        public async Task AddAsync (Dish dish)
        {
            _context.Dishes.Add(dish);
            await Task.CompletedTask;
        }

        public void Update (Dish dish)
        {
            _context.Entry(dish).State = EntityState.Modified;
        }

        public void Delete (Dish dish)
        {
            _context.Dishes.Remove(dish);
        }

        public async Task<IEnumerable<Dish>> GetByCategoryAsync (int categoryId)
        {
            return await _context.Dishes
            .Where(d => d.CategoryId == categoryId).ToListAsync();
        }
    }
}