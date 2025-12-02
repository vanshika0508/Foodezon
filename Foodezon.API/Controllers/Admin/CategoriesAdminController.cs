using Foodezon.Api.Models;
using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers.Admin
{
    [Microsoft.AspNetCore.Components.Route("Admin/[controller]/[action]")]
    public class CategoriesAdminController : AdminBaseController
    {
        private readonly ICategoryAdminService _categoryService;

        public CategoriesAdminController(ICategoryAdminService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var cats = await _categoryService.GetAllAsync();
            return View(cats);
        }

        [HttpGet]
        public IActionResult Create() => View(new CategoryFormViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(CategoryFormViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var c = new Category {Name = vm.Name, Description = vm.Description};
            await _categoryService.CreateAsync(c);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var c = await _categoryService.GetByIdAsync(id);
            if (c == null) return NotFound();
            var vm = new CategoryFormViewModel {Id = c.Id, Name = c.Name, Description = c.Description};
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var c = await _categoryService.GetByIdAsync(vm.Id);
            if (c == null) return NotFound();
            c.Name = vm.Name;
            c.Description = vm.Description;
            await _categoryService.UpdateAsync(c);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _categoryService.GetByIdAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}