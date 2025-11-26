using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Web.Controllers
{
    public class AdminCategoryController : Controller
    {
        private readonly FoodezonDbContext _db;

        public AdminCategoryController(FoodezonDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var Categories = await _db.Categories.ToListAsync();
            return View(Categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(category);
            }

            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category updated)
        {
            if (!ModelState.IsValid)
                return View(updated);

            var category = await _db.Categories.FindAsync(updated.CategoryId);
            if (category == null) return NotFound();

            category.Name = updated.Name;
            category.Details = updated.Details;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}