using System.Collections.Generic;
using System.Threading.Tasks;
using Foodezon.Core.DTOs.Orders;

namespace Foodezon.Core.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CheckoutAsync(CheckoutRequestDto request);
        Task<OrderDto?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(int userId);
    }
}
