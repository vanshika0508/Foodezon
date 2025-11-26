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

        

        [HttpGet("/Cart")]
        public async Task<IActionResult> Index()
        {
            var userId = await GetOrCreateUserIdAsync();
            var cart = await _cartService.GetCartForUserAsync(userId);
            return View("Index", cart);
        }

        [HttpPost("/Cart/Add")]
        public async Task<IActionResult> AddToCart(int dishId, int quantity = 1)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.AddToCartAsync(userId, dishId, quantity);
            return RedirectToAction("Index", "Menu");
        }

        [HttpPost("/Cart/Update")]
        public async Task<IActionResult> UpdateQuantity(int dishId, int quantity)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.UpdateCartItemAsync(userId, dishId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost("/Cart/Remove")]
        public async Task<IActionResult> RemoveItem(int dishId)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.RemoveItemAsync(userId, dishId);
            return RedirectToAction("Index");
        }

        [HttpPost("/Cart/Clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.ClearCartAsync(userId);
            return RedirectToAction("Index");
        }



        

        [HttpGet("api/cart/{userId:int}")]
        public async Task<IActionResult> GetCartApi(int userId)
        {
            var cart = await _cartService.GetCartForUserAsync(userId);
            return Ok(cart); // JSON
        }

        [HttpPost("api/cart/add")]
        public async Task<IActionResult> AddToCartApi([FromBody] CartItemRequest request)
        {
            var userId = request.UserId;
            await _cartService.AddToCartAsync(userId, request.DishId, request.Quantity);
            var updatedCart = await _cartService.GetCartForUserAsync(userId);
            return Ok(updatedCart);
        }

        [HttpPost("api/cart/update")]
        public async Task<IActionResult> UpdateCartApi([FromBody] CartItemRequest request)
        {
            var userId = request.UserId;
            await _cartService.UpdateCartItemAsync(userId, request.DishId, request.Quantity);
            var updatedCart = await _cartService.GetCartForUserAsync(userId);
            return Ok(updatedCart);
        }

        [HttpPost("api/cart/remove")]
        public async Task<IActionResult> RemoveCartItemApi([FromBody] CartItemRequest request)
        {
            var userId = request.UserId;
            await _cartService.RemoveItemAsync(userId, request.DishId);
            var updatedCart = await _cartService.GetCartForUserAsync(userId);
            return Ok(updatedCart);
        }

        [HttpPost("/api/cart/clear")]
        public async Task<IActionResult> ClearCartApi([FromBody] int userId)
        {
            await _cartService.ClearCartAsync(userId);
            var updatedCart = await _cartService.GetCartForUserAsync(userId);
            return Ok(updatedCart);
        }
    }


    
    public class CartItemRequest
    {
        public int UserId { get; set; }
        public int DishId { get; set; }
        public int Quantity { get; set; }
    }
}
