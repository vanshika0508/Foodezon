using Foodezon.Core.Models;

namespace Foodezon.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync (Category category);
        void Update (Category category);
        void Delete (Category category);
    }
}