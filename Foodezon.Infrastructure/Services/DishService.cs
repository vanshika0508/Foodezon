using Foodezon.Core.DTOs.Dishes;
using Foodezon.Core.Interfaces;

namespace Foodezon.Infrastructure.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;

        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }
        public async Task<IEnumerable<DishDto>> GetAllAvailableDishesAsync()
        {
            var dishes = await _dishRepository.GetAllAvailableAsync();

            return dishes.Select(d => new DishDto
            {
                Id           = d.Id,
                Name         = d.Name,
                Description  = d.Description,
                Price        = d.Price,
                ImageUrl     = d.ImageUrl,
                IsAvailable  = d.IsAvailable,
                CategoryName = d.Category != null ? d.Category.Name : string.Empty
            });
        }

        public async Task<DishDto?> GetDishByIdAsync(int id)
        {
            var d = await _dishRepository.GetByIdAsync(id);
            if (d == null) return null;

            return new DishDto
            {
                Id           = d.Id,
                Name         = d.Name,
                Description  = d.Description,
                Price        = d.Price,
                ImageUrl     = d.ImageUrl,
                IsAvailable  = d.IsAvailable,
                CategoryName = d.Category != null ? d.Category.Name : string.Empty
            };
        }
        }
    }
