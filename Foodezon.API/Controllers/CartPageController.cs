using System.Linq;
using System.Threading.Tasks;
using Foodezon.Api.Models.ViewModels;
using Foodezon.Core.Interfaces;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    public class CartPageController : BaseMvcController
    {
        private readonly ICartService _cartService;

        public CartPageController(
            ApplicationDbContext context,
            ICartService cartService) : base(context)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = await GetOrCreateUserIdAsync();
            var cartDto = await _cartService.GetCartForUserAsync(userId);

            var vm = new CartViewModel
            {
                Items = cartDto.Items.Select(i => new CartItemViewModel
                {
                    DishId = i.DishId,
                    DishName = i.DishName,
                    ImageUrl = i.ImageUrl,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int dishId, int quantity)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.UpdateCartItemAsync(userId, dishId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int dishId)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.RemoveItemAsync(userId, dishId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GoToCheckout()
        {
            return RedirectToAction("Index", "Checkout");
        }
    }
}
