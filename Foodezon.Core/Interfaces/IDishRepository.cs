
using Foodezon.Core.Models;

namespace Foodezon.Core.Interfaces
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAllAvailableAsync();
        Task<Dish?> GetByIdAsync(int id);
    }
}
