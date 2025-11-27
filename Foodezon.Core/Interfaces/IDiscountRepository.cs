using Foodezon.Core.Models;

namespace Foodezon.Core.Interfaces
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Discount>> GetDiscountsAsync();
        Task<Discount> GetByIdAsync (int id);
        Task AddAsync (Discount discount);
        void Update(Discount discount);
        void Delete(Discount discount);
        Task<IEnumerable<Discount>> GetActiveDiscountsAsync();
    }
}