using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly FoodezonDbContext _db;
        public AdminController(FoodezonDbContext db) => _db = db;

        // Dishes

        [HttpGet("dishes")]
        public async Task<IActionResult> GetDishes(int id)
        {
            var dishes = await _db.Dishes.Include(d => d.CategoryId).FirstOrDefaultAsync(d => d.DishId == id);
            return Ok(dishes);
        }

        [HttpGet("dish/{id:int}")]
        public async Task<IActionResult>GetDish(int id)
        {
            var dish = await _db.Dishes.Include(d => d.CategoryId).FirstOrDefaultAsync(d => d.DishId == id);
            if (dish == null) return NotFound();
            return Ok(dish);
        }

        [HttpPost("dish")]
        public async Task<IActionResult> CreateDish([FromBody] Dish model)
        {
            if (!ModelState.IsValid) return BadRequest (ModelState);
            _db.Dishes.Add(model);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("dish/{id:int}")]
        public async Task<IActionResult> UpdateDish(int id, [FromBody] Dish model)
        {
            var dish = await _db.Dishes.FindAsync(id);
            if (dish == null) return NotFound();

            dish.Name = model.Name;
            dish.Details = model.Details;
            dish.Price = model.Price;
            dish.CategoryId = model.CategoryId;

            await _db.SaveChangesAsync();
            return Ok(dish);
        }

        [HttpDelete("dish/{id:int}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var dish = await _db.Dishes.FindAsync(id);
            if (dish == null) return NotFound();
            _db.Dishes.Remove(dish);
            await _db.SaveChangesAsync();
            return Ok( new { message = "Deleted"});
        }

        // Discounts

        [HttpPost("dish/{dishId:int}/discount")]
        public async Task<IActionResult> AddDiscount (int dishId, [FromBody] Discount model)
        {
            var dish = await _db.Dishes.FindAsync(dishId);
            if (dish == null) return NotFound();

            model.DishId = dishId;
            _db.Discounts.Add(model);
            await _db.SaveChangesAsync();
            return Ok(model);
        }

        [HttpDelete("discount/{discountId:int}")]
        public async Task<IActionResult> DeleteDiscount (int discountId)
        {
            var d = await _db.Discounts.FindAsync(discountId);
            if (d == null) return NotFound();

            _db.Discounts.Remove(d);
            await _db.SaveChangesAsync();
            return Ok (new { message = "Discount Removed"});
        }
    }
}