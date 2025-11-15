using System.Threading.Tasks;
using Foodezon.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishesController(IDishService dishService)
        {
            _dishService = dishService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dishes = await _dishService.GetAllAvailableDishesAsync();
            return Ok(dishes);
        }

       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dish = await _dishService.GetDishByIdAsync(id);

            if (dish == null)
                return NotFound();

            return Ok(dish);
        }
    }
}
