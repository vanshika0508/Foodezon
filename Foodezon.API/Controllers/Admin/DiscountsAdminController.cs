using Foodezon.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
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
        public IActionResult Create() => View();
    }
}