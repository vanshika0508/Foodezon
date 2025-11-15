using System.Collections.Generic;
using System.Threading.Tasks;
using Foodezon.Core.Models;

namespace Foodezon.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task AddAsync(Order order);
        Task<int> SaveChangesAsync();
    }
}
