using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
            var dishes = await _db.Dishes.ToListAsync();
            return Ok(dishes);
        }

        [HttpGet("dish/{id:int}")]
        public async Task<IActionResult>GetDish(int id)
        {
            var dish = await _db.Dishes.FirstOrDefaultAsync(d => d.DishId == id);
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

        // Delete a certain discount
        [HttpDelete("discount/{discountId:int}")]
        public async Task<IActionResult> DeleteDiscount (int discountId)
        {
            var d = await _db.Discounts.FindAsync(discountId);
            if (d == null) return NotFound();

            _db.Discounts.Remove(d);
            await _db.SaveChangesAsync();
            return Ok (new { message = "Discount Removed"});
        }

        // Get all discounts for a certain dish
        [HttpGet("dish/{dishId:int}/discounts")]
        public async Task<IActionResult> GetDiscountsForDish(int dishId)
        {
            var discounts = await _db.Discounts
            .Where(d => d.DishId == dishId).ToListAsync();

            return Ok(discounts);
        }

        // Get the active discounts for a dish
        [HttpGet("dish/{dishId:int}/discounts/active")]
        public async Task<IActionResult> GetActiveDiscounts(int dishId)
        {
            var discounts = await _db.Discounts
            .Where(d => d.DishId == dishId && d.ActiveStatus)
            .ToListAsync();

            return Ok(discounts);
        }

        //    ORDERS

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _db.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("order/{orderId:int}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] Order model)
        {
            if (!ModelState.IsValid) return BadRequest (ModelState);
            _db.Orders.Add(model);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("order/{orderId:int}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] Order model)
        {
            var existingOrder = await _db.Orders.FindAsync(orderId);

            if (existingOrder == null) return NotFound();

            existingOrder.CustomerName = model.CustomerName;
            existingOrder.CustomerEmail = model.CustomerEmail;
            existingOrder.OrderStatus = model.OrderStatus;
            existingOrder.orderDate = model.orderDate;

            await _db.SaveChangesAsync();

            return Ok(existingOrder);
        }

        [HttpDelete("order/{orderId:int}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order == null) return NotFound();

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return Ok(new {message = "Order deleted successfully"});
        }
    }
}