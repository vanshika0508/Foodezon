using Foodezon.Core.DTOs.Cart;
using Foodezon.Core.Interfaces;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    public class CartController : BaseMvcController
    {
        private readonly ICartService _cartService;

        public CartController(ApplicationDbContext context, ICartService cartService)
            : base(context)
        {
            _cartService = cartService;
        }

        // SHOW CART PAGE
        [HttpGet("/Cart")]
        public async Task<IActionResult> Index()
        {
            var userId = await GetOrCreateUserIdAsync();
            var cart = await _cartService.GetCartForUserAsync(userId);
            return View("Index", cart);
        }

        // ADD ITEM FROM MENU
        [HttpPost("/Cart/Add")]
        public async Task<IActionResult> AddToCart(int dishId, int quantity = 1)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.AddToCartAsync(userId, dishId, quantity);

            return RedirectToAction("Index", "Menu");
        }

        // UPDATE QUANTITY
        [HttpPost("/Cart/Update")]
        public async Task<IActionResult> UpdateQuantity(int dishId, int quantity)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.UpdateCartItemAsync(userId, dishId, quantity);
            return RedirectToAction("Index");
        }

        // REMOVE ITEM
        [HttpPost("/Cart/Remove")]
        public async Task<IActionResult> RemoveItem(int dishId)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.RemoveItemAsync(userId, dishId);
            return RedirectToAction("Index");
        }

        // CLEAR CART
        [HttpPost("/Cart/Clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.ClearCartAsync(userId);
            return RedirectToAction("Index");
        }
    }
}
