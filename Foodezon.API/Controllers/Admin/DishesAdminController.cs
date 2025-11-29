using Foodezon.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var categories = (await _categoryService.GetAllAsync()).Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            var vm = new DishFormViewModel {categories = categories};
            return View(vm);
        }
    }
}