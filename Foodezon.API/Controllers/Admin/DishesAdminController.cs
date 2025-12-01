using Foodezon.Api.Models;
using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;

namespace Foodezon.Api.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class DishesAdminController : AdminBaseController
    {
        private readonly IDishAdminService _dishService;
        private readonly ICategoryAdminService _categoryService;

        public DishesAdminController(IDishAdminService dishService, ICategoryAdminService categoryService)
        {
            _dishService = dishService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var dishes = await _dishService.GetAllAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cats = (await _categoryService.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            var vm = new DishFormViewModel {Categories = cats};
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DishFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = (await _categoryService.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
                return View(vm);
            }

            var dish = new Dish
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                ImageUrl = vm.ImageUrl ?? string.Empty,
                IsAvailable = vm.IsAvailable,
                CategoryId = vm.CategoryId
            };

            await _dishService.CreateAsync(dish);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dish = await _dishService.GetByIdAsync(id);
            if (dish == null) return NotFound();

            var cats = (await _categoryService.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            var vm = new DishFormViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                ImageUrl = dish.ImageUrl,
                IsAvailable = dish.IsAvailable,
                CategoryId = dish.CategoryId,
                Categories = cats
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DishFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = (await _categoryService.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
                return View(vm);
            }

            var dish = await _dishService.GetByIdAsync(vm.Id);
            if (dish == null) return NotFound();

            dish.Name = vm.Name;
            dish.Description = vm.Description;
            dish.Price = vm.Price;
            dish.ImageUrl = vm.ImageUrl ?? string.Empty;
            dish.IsAvailable = vm.IsAvailable;
            dish.CategoryId = vm.CategoryId;

            await _dishService.UpdateAsync(dish);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dish = await _dishService.GetByIdAsync(id);
            if (dish == null) return NotFound();
            return View(dish);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _dishService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}