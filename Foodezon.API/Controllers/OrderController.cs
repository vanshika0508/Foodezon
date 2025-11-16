using System.Threading.Tasks;
using Foodezon.Core.DTOs.Orders;
using Foodezon.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var order = await _orderService.CheckoutAsync(request);
                return Ok(order);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

       
        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();

            return Ok(order);
        }

       
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetForUser(int userId)
        {
            var orders = await _orderService.GetOrdersForUserAsync(userId);
            return Ok(orders);
        }
    }
}
