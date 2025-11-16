
using Foodezon.Core.DTOs.Orders;
using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ApplicationDbContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> CheckoutAsync(CheckoutRequestDto request)
        {
            
            var user = await _context.Users
                .Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems)
                        .ThenInclude(ci => ci.Dish)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
                throw new Exception("User not found.");

            if (user.Cart == null || !user.Cart.CartItems.Any())
                throw new Exception("Cart is empty.");

            var cart = user.Cart;

            
            var subtotal = cart.CartItems.Sum(ci => ci.Dish.Price * ci.Quantity);

            
            Discount? discount = null;
            decimal discountAmount = 0;

            if (!string.IsNullOrWhiteSpace(request.DiscountCode))
            {
                var code = request.DiscountCode.Trim();
                var now = DateTime.UtcNow;

                discount = await _context.Discounts
                    .FirstOrDefaultAsync(d =>
                        d.code == code &&
                        d.IsActive &&
                        d.ValidFrom <= now &&
                        d.ValidTo >= now);

                if (discount != null)
                {
                    discountAmount = Math.Round(subtotal * (discount.Percentage / 100m), 2);
                }
            }

            var total = subtotal - discountAmount;
            if (total < 0) total = 0;


            var order = new Order
            {
                UserId = user.Id,
                DiscountId = discount?.Id,
                Subtotal = subtotal,
                Discountamount = discountAmount,
                TotalAmount = total,
                Status = OrderStatus.Pending,
                DeliveryAddress = string.IsNullOrWhiteSpace(request.DeliveryAddress)
                                    ? user.Address
                                    : request.DeliveryAddress!.Trim(),
                PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber)
                                    ? user.PhoneNumber
                                    : request.PhoneNumber!.Trim()
            };

            
            foreach (var ci in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    DishId = ci.DishId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Dish.Price
                };
                order.OrderItems.Add(orderItem);
            }

       
            await _orderRepository.AddAsync(order);

           
            cart.CartItems.Clear();

            await _orderRepository.SaveChangesAsync();

            
            return MapToOrderDto(order, discount);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return null;

            return MapToOrderDto(order, order.Discount);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);

            return orders.Select(o => MapToOrderDto(o, o.Discount));
        }

        private OrderDto MapToOrderDto(Order order, Discount? discount)
        {
            var items = order.OrderItems.Select(oi => new OrderItemDto
            {
                DishId = oi.DishId,
                DishName = oi.Dish?.Name ?? string.Empty,
                UnitPrice = oi.UnitPrice,
                Quantity = oi.Quantity,
                LineTotal = oi.UnitPrice * oi.Quantity
            }).ToList();

            return new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                UserId = order.UserId,
                Subtotal = order.Subtotal,
                DiscountAmount = order.Discountamount,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                DeliveryAddress = order.DeliveryAddress,
                PhoneNumber = order.PhoneNumber,
                CreatedAt = order.CreatedAt,
                Items = items,
                DiscountCode = discount?.code
            };
        }
    }
}
