using Foodezon.Api.Models;
using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers.Admin
{
    //[Route("Admin/[controller]/[action]")]
    public class DiscountsAdminController : AdminBaseController
    {
        private readonly IDiscountAdminService _discountService;

        public DiscountsAdminController(IDiscountAdminService discountService)
        {
            _discountService = discountService;
        }

        public async Task<IActionResult> Index()
        {
            var discounts = await _discountService.GetAllAsync();
            return View(discounts);
        }

        [HttpGet]
        public IActionResult Create() => View(new DiscountFormViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(DiscountFormViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var d = new Discount
            {
                code = vm.Code.Trim(),
                Description = vm.Description,
                Percentage = vm.Percentage,
                ValidFrom = vm.ValidFrom,
                ValidTo = vm.ValidTo,
                IsActive = vm.IsActive
            };
            await _discountService.CreateAsync(d);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var d = await _discountService.GetByIdAsync(id);
            if (d == null) return NotFound();

            var vm = new DiscountFormViewModel
            {
                Code = d.code,
                Id = d.Id,
                Description = d.Description,
                Percentage = d.Percentage,
                ValidFrom = d.ValidFrom,
                ValidTo = d.ValidTo,
                IsActive = d.IsActive
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DiscountFormViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var d = await _discountService.GetByIdAsync(vm.Id);
            if (d == null) return NotFound();

            d.code = vm.Code.Trim();
            d.Description = vm.Description;
            d.Percentage = vm.Percentage;
            d.ValidFrom = vm.ValidFrom;
            d.ValidTo = vm.ValidTo;
            d.IsActive = vm.IsActive;

            await _discountService.UpdateAsync(d);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var d = await _discountService.GetByIdAsync(id);
            if (d == null) return NotFound();
            return View(d);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _discountService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}