using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Web.Controllers
{
    public class AdminDiscountController : Controller
    {
        private readonly FoodezonDbContext _db;

        public AdminDiscountController(FoodezonDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var discounts = await _db.Discounts
                .Include(d => d.Dish)
                .ToListAsync();

            return View(discounts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Dishes = await _db.Dishes.ToListAsync();
            return View();
        }

        public async Task<IActionResult> Create(Discount discount)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Dishes = await _db.Dishes.ToListAsync();
                return View(discount);
            }

                _db.Discounts.Add(discount);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var discount = await _db.Discounts.FindAsync(id);
            if (discount == null) return NotFound();

            ViewBag.Dishes = await _db.Dishes.ToListAsync();
            return View(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Discount updated)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Dishes = await _db.Dishes.ToListAsync();
                return View(updated);
            }

            var discount = await _db.Discounts.FindAsync(updated.DiscountId);
            if (discount == null) return NotFound();

            discount.DiscountCode = updated.DiscountCode;
            discount.Percentage = updated.Percentage;
            discount.DishId = updated.DishId;
            discount.StartDate = updated.StartDate;
            discount.EndDate = updated.EndDate;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var discount = await _db.Discounts.FindAsync(id);
            if (discount == null) return NotFound();

            _db.Discounts.Remove(discount);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
    