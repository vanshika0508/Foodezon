using Foodezon.Core.DTOs.Cart;
using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto> GetCartForUserAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Dish)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return MapToCartDto(cart);
        }

        public async Task<CartDto> AddToCartAsync(int userId, int dishId, int quantity)
        {
            // Get user's cart
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // Create cart if missing
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Check if dish exists
            var dish = await _context.Dishes.FindAsync(dishId);
            if (dish == null || !dish.IsAvailable)
                throw new Exception("Dish not available.");

            // Check if cart already has this dish
            var existingItem = cart.CartItems.SingleOrDefault(ci => ci.DishId == dishId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    DishId = dishId,
                    Quantity = quantity
                });
            }

            await _context.SaveChangesAsync();

            // Reload cart with dish details
            cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Dish)
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            return MapToCartDto(cart);
        }

        public async Task<CartDto> UpdateCartItemAsync(int userId, int dishId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Dish)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                throw new Exception("Cart not found.");

            var item = cart.CartItems.SingleOrDefault(ci => ci.DishId == dishId);
            if (item == null)
                throw new Exception("Item not in cart.");

            if (quantity <= 0)
                cart.CartItems.Remove(item);
            else
                item.Quantity = quantity;

            await _context.SaveChangesAsync();
            return MapToCartDto(cart);
        }

        public async Task<CartDto> RemoveItemAsync(int userId, int dishId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Dish)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                var item = cart.CartItems.SingleOrDefault(ci => ci.DishId == dishId);
                if (item != null)
                {
                    cart.CartItems.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return MapToCartDto(cart);
        }

            public async Task<CartDto> ClearCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                cart.CartItems.Clear();
                await _context.SaveChangesAsync();
            }
            else
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return MapToCartDto(cart);
        }

    
        private CartDto MapToCartDto(Cart cart)
        {
            var items = cart.CartItems.Select(ci => new CartItemDto
            {
                DishId    = ci.DishId,
                DishName  = ci.Dish?.Name ?? string.Empty,
                UnitPrice = ci.Dish?.Price ?? 0,
                Quantity  = ci.Quantity,
                LineTotal = (ci.Dish?.Price ?? 0) * ci.Quantity,
                ImageUrl  = ci.Dish?.ImageUrl ?? string.Empty
            }).ToList();

            return new CartDto
            {
                CartId     = cart.Id,
                UserId     = cart.UserId,
                Items      = items,
                TotalPrice = items.Sum(i => i.LineTotal)
            };
        }
    }
}
