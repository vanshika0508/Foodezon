using Foodezon.Api.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Api.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class AdminAuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string SessionAdminKey = "IsAdmin";

        public AdminAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var email = model.Email?.Trim().ToLower();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.Role != Core.Models.UserRole.Admin)
            {
                ModelState.AddModelError("", "Invalid admin credentials.");
                return View(model);
            }

            HttpContext.Session.SetInt32(SessionAdminKey, 1);
            HttpContext.Session.SetInt32("AdminUserId", user.Id);

            return RedirectToAction("Index", "Dashboard");
        }


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionAdminKey);
            HttpContext.Session.Remove("AdminUserId");
            return RedirectToAction("Login");
        }
    }
}