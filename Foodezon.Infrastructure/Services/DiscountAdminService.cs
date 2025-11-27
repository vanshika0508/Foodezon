using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Services
{
    public class DiscountAdminService : IDiscountAdminService
    {
        private readonly ApplicationDbContext _context;

        public DiscountAdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount?> GetByIdAsync(int id)
        {
            return await _context.Discounts.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Discount> CreateAsync(Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task UpdateAsync(Discount discount)
        {
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var disc = await _context.Discounts.FindAsync(id);
            if (disc != null)
            {
                _context.Discounts.Remove(disc);
                await _context.SaveChangesAsync();
            }
        }
    }
}