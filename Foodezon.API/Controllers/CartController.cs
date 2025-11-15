using System.Threading.Tasks;
using Foodezon.Core.DTOs.Cart;
using Foodezon.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/cart
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart/{userId}
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartForUserAsync(userId);
            return Ok(cart);
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = await _cartService.AddToCartAsync(request.UserId, request.DishId, request.Quantity);
            return Ok(cart);
        }

        // PUT: api/cart/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateCartItemRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = await _cartService.UpdateCartItemAsync(request.UserId, request.DishId, request.Quantity);
            return Ok(cart);
        }

        // DELETE: api/cart/{userId}/items/{dishId}
        [HttpDelete("{userId:int}/items/{dishId:int}")]
        public async Task<IActionResult> RemoveItem(int userId, int dishId)
        {
            var cart = await _cartService.RemoveItemAsync(userId, dishId);
            return Ok(cart);
        }

        // DELETE: api/cart/{userId}/clear
        [HttpDelete("{userId:int}/clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var cart = await _cartService.ClearCartAsync(userId);
            return Ok(cart);
        }
    }
}
