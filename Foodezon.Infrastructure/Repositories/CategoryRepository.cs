using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FoodezonDbContext _context;

        public CategoryRepository(FoodezonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddAsync (Category category)
        {
            _context.Categories.Add(category);
            await Task.CompletedTask;
        }

        public void Update (Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }

        public void Delete (Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}