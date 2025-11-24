using System.Threading.Tasks;
using Foodezon.Api.Controllers;
using Foodezon.Api.Models.ViewModels;
using Foodezon.Core.Interfaces;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Foodezom.Api.Controllers
{
    public class MenuController : BaseMvcController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICartService _cartService;

        public MenuController(
            ApplicationDbContext context,
            ICategoryService categoryService,
            ICartService cartService) : base(context)
        {
            _categoryService = categoryService;
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await GetOrCreateUserIdAsync();

            var categories = await _categoryService.GetAllAsync();
            var vm = new MenuViewModel
            {
                Categories = new System.Collections.Generic.List<Foodezon.Core.DTOs.Categories.CategoryDto>(categories)
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int dishId, int quantity = 1)
        {
            var userId = await GetOrCreateUserIdAsync();
            await _cartService.AddToCartAsync(userId, dishId, quantity);

            // Stay on menu for now
            return RedirectToAction("Index");
        }
}
}