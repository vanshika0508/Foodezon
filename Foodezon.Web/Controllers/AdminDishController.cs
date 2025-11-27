using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Web.Controllers
{
    public class AdminDishController : Controller
    {
        private readonly FoodezonDbContext _db;
        public AdminDishController(FoodezonDbContext db) => _db = db;

        // Dishes

        public async Task<IActionResult> Index()
        {
            var dishes = await _db.Dishes
            .Include(d => d.Category)
            .ToListAsync();

            return View(dishes);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Dish dish)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(dish);
            }

                _db.Dishes.Add(dish);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dish = await _db.Dishes.FindAsync(id);
            if (dish == null) return NotFound();

            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View(dish);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (Dish updated)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(updated);
            }

            var dish = await _db.Dishes.FindAsync(updated.DishId);
            if (dish == null) return NotFound();

            dish.Name = updated.Name;
            dish.Details = updated.Details;
            dish.Price = updated.Price;
            dish.CategoryId = updated.CategoryId;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dish = await _db.Dishes.FindAsync(id);
            if (dish == null) return NotFound();

            _db.Dishes.Remove(dish);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}