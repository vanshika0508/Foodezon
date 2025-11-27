using Foodezon.Core.Models;

namespace Foodezon.Core.Interfaces
{
    public interface IDishAdminService
    {
        Task<IEnumerable<Dish>> GetAllAsync();
        Task<Dish?> GetByIdAsync(int id);
        Task<Dish> CreateAsync(Dish dish);
        Task UpdateAsync (Dish dish);
        Task DeleteAsync (int id);
    }

    public interface ICategoryAdminService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task UpdateAsync (Category category);
        Task DeleteAsync (int id);
    }

    public interface IDiscountAdminService
    {
        Task<IEnumerable<Discount>> GetAllAsync();
        Task<Discount?> GetByIdAsync(int id);
        Task<Discount> CreateAsync(Discount discount);
        Task UpdateAsync (Discount discount);
        Task DeleteAsync (int id);
    }
}