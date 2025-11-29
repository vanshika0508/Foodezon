using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Api.Controllers.Admin
{
    [Microsoft.AspNetCore.Mvc.Route("Admin/[controller]/[action]")]
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
        public async Task<IActionResult> Login(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Email is required");
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.Trim().ToLower());
            if (user == null || user.Role != Core.Models.UserRole.Admin)
            {
                ModelState.AddModelError("", "Could not find admin or user is not an admin.");
                return View();
            }

            HttpContext.Session.SetInt32(SessionAdminKey, 1);
            HttpContext.Session.SetInt32("AdminUserId", user.Id);

            return RedirectToAction("Index", "Dashbaord", new {area = "", controller = "Dashboard"});
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