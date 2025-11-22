using Foodezon.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foodezon.Core.Interfaces
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetDishesAsync();
        Task<Dish> GetByIdAsync (int id);
        Task AddAsync (Dish dish);
        void Update(Dish dish);
        void Delete(Dish dish);
        Task<IEnumerable<Dish>> GetByCategoryAsync (int CategoryId);
    }
}