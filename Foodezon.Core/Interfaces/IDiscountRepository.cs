using Foodezon.Core.Models;

namespace Foodezon.Core.Interfaces
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Discount>> GetDiscountsAsync();
        Task<Discount> GetByIdAsync (int id);
        Task AddAsync (Discount discount);
        void Update(Dish dish);
        void Delete(Dish dish);
        Task<IEnumerable<Discount>> GetActiveDiscountsAsync();
    }
}