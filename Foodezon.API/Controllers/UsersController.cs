
using Foodezon.Core.DTOs.Users;
using Foodezon.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _userService.CreateUserAsync(
                    dto.FirstName,
                    dto.LastName,
                    dto.Email,
                    dto.PhoneNumber,
                    dto.Address);

                var response = new UserResponseDto
                {
                    Id          = user.Id,
                    FirstName   = user.FirstName,
                    LastName    = user.LastName,
                    Email       = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address     = user.Address,
                    Role        = user.Role.ToString()
                };

                
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, response);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();

            var response = users.Select(u => new UserResponseDto
            {
                Id          = u.Id,
                FirstName   = u.FirstName,
                LastName    = u.LastName,
                Email       = u.Email,
                PhoneNumber = u.PhoneNumber,
                Address     = u.Address,
                Role        = u.Role.ToString()
            });

            return Ok(response);
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var dto = new UserResponseDto
            {
                Id          = user.Id,
                FirstName   = user.FirstName,
                LastName    = user.LastName,
                Email       = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address     = user.Address,
                Role        = user.Role.ToString()
            };

            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.UpdateUserAsync(
                id,
                dto.FirstName,
                dto.LastName,
                dto.PhoneNumber,
                dto.Address);

            if (user == null) return NotFound();

            var response = new UserResponseDto
            {
                Id          = user.Id,
                FirstName   = user.FirstName,
                LastName    = user.LastName,
                Email       = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address     = user.Address,
                Role        = user.Role.ToString()
            };

            return Ok(response);
        }

    
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted) return NotFound();

            return NoContent(); 
        }
    }
}
