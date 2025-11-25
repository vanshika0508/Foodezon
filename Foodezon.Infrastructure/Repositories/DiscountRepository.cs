using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly FoodezonDbContext _context;

        public DiscountRepository(FoodezonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discount>> GetDiscountsAsync()
        {
            return await _context.Discounts
            .Include(d => d.Dish)
            .ToListAsync();
        }

        public async Task<Discount> GetByIdAsync(int id)
        {
            return await _context.Discounts
            .Include(_d => _d.Dish)
            .FirstOrDefaultAsync(d => d.DiscountId == id);
        }

        public async Task AddAsync (Discount discount)
        {
            _context.Discounts.Add(discount);
            await Task.CompletedTask;
        }

        public void Update (Discount discount)
        {
            _context.Entry(discount).State = EntityState.Modified;
        }

        public void Delete (Discount discount)
        {
            _context.Discounts.Remove(discount);
        }

        public async Task<IEnumerable<Discount>> GetActiveDiscountsAsync()
        {
            return await _context.Discounts
            .Where(d => d.ActiveStatus)
            .Include(d => d.DishId)
            .ToListAsync();
        }
    }
}