
using Foodezon.Core.DTOs.Cart;

namespace Foodezon.Core.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartForUserAsync(int userId);
        Task<CartDto> AddToCartAsync(int userId, int dishId, int quantity);
        Task<CartDto> UpdateCartItemAsync(int userId, int dishId, int quantity);
        Task<CartDto> RemoveItemAsync(int userId, int dishId);
        Task<CartDto> ClearCartAsync(int userId);
    }
}
