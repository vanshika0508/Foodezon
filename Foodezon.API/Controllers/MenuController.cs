using System.Threading.Tasks;
using Foodezon.Api.Models.ViewModels;
using Foodezon.Core.Interfaces;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    public class MenuController : BaseMvcController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICartService _cartService;

        public MenuController(
            ApplicationDbContext context,
            ICategoryService categoryService,
            ICartService cartService
        ) : base(context)
        {
            _categoryService = categoryService;
            _cartService = cartService;
        }

        // SHOW MENU PAGE
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await GetOrCreateUserIdAsync();

            var categories = await _categoryService.GetAllAsync();

            var vm = new MenuViewModel
            {
                Categories = new System.Collections.Generic.List<Core.DTOs.Categories.CategoryDto>(categories)
            };

            return View(vm);
        }

        // ADD TO CART
        [HttpPost]
        public async Task<IActionResult> AddToCart(int dishId, int quantity = 1)
        {
            // Ensure valid quantity
            if (quantity <= 0)
                quantity = 1;

            var userId = await GetOrCreateUserIdAsync();

            await _cartService.AddToCartAsync(userId, dishId, quantity);

            return RedirectToAction("Index");
        }
    }
}
