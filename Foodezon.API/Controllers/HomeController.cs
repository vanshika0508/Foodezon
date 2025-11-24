using System;
using System.Linq;
using System.Threading.Tasks;
using Foodezon.Api.Models.ViewModels;
using Foodezon.Core.Interfaces;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    public class HomeController : BaseMvcController
    {
        private readonly ICategoryService _categoryService;
        private readonly IDishService _dishService;

        public HomeController(
            ApplicationDbContext context,
            ICategoryService categoryService,
            IDishService dishService) : base(context)
        {
            _categoryService = categoryService;
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm)
        {
            
            await GetOrCreateUserIdAsync();

            var categories = (await _categoryService.GetAllAsync()).ToList();
            var allDishes = (await _dishService.GetAllAvailableDishesAsync()).ToList();

            var vm = new HomeViewModel
            {
                Categories = categories,
                SearchTerm = searchTerm
            };

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                vm.SearchResults = allDishes
                    .Where(d =>
                        d.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        d.Description.Contains(term, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(vm);
        }
    }
}
