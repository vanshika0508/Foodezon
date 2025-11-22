using System;
using System.Threading.Tasks;
using Foodezon.Core.Models;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foodezon.Api.Controllers
{
    public abstract class BaseMvcController : Controller
    {
        protected readonly ApplicationDbContext _context;

        protected BaseMvcController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected async Task<int> GetOrCreateUserIdAsync()
        {
            const string key = "UserId";
            var existing = HttpContext.Session.GetInt32(key);
            if (existing.HasValue)
                return existing.Value;

            // Create a guest user
            var guest = new User
            {
                FirstName = "Guest",
                LastName = "",
                Email = $"guest_{Guid.NewGuid():N}@foodezon.local",
                PhoneNumber = "",
                Address = ""
            };

            _context.Users.Add(guest);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32(key, guest.Id);
            return guest.Id;
        }
    }
}
