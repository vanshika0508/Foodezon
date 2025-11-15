
using Foodezon.Core.DTOs.Dishes;

namespace Foodezon.Core.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishDto>> GetAllAvailableDishesAsync();
        Task<DishDto?> GetDishByIdAsync(int id);
    }
}
