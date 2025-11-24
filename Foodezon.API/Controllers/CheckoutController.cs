using System.Threading.Tasks;
using Foodezon.Api.Models.ViewModels;
using Foodezon.Core.DTOs.Orders;
using Foodezon.Core.Interfaces;
using Foodezon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foodezon.Api.Controllers
{
    public class CheckoutController : BaseMvcController
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutController(
            ApplicationDbContext context,
            ICartService cartService,
            IOrderService orderService) : base(context)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = await GetOrCreateUserIdAsync();
            var cartDto = await _cartService.GetCartForUserAsync(userId);

            if (cartDto.Items.Count == 0)
                return RedirectToAction("Index", "Menu");

            var cartVm = new CartViewModel
            {
                Items = cartDto.Items.ConvertAll(i => new CartItemViewModel
                {
                    DishId = i.DishId,
                    DishName = i.DishName,
                    ImageUrl = i.ImageUrl,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                })
            };

            var user = await _context.Users.FirstAsync(u => u.Id == userId);

            var vm = new CheckoutViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Cart = cartVm
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            var userId = await GetOrCreateUserIdAsync();
            var cartDto = await _cartService.GetCartForUserAsync(userId);

            if (cartDto.Items.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty.");
            }

            if (!ModelState.IsValid)
            {
                model.Cart = new CartViewModel
                {
                    Items = cartDto.Items.ConvertAll(i => new CartItemViewModel
                    {
                        DishId = i.DishId,
                        DishName = i.DishName,
                        ImageUrl = i.ImageUrl,
                        UnitPrice = i.UnitPrice,
                        Quantity = i.Quantity
                    })
                };
                return View(model);
            }

            // Update user info
            var user = await _context.Users.FirstAsync(u => u.Id == userId);
            var nameParts = (model.FullName ?? "").Trim().Split(' ', 2);
            user.FirstName = nameParts.Length > 0 ? nameParts[0] : "Guest";
            user.LastName = nameParts.Length > 1 ? nameParts[1] : "";
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            await _context.SaveChangesAsync();

            // Create order via service
            var request = new CheckoutRequestDto
            {
                UserId = userId,
                DiscountCode = string.IsNullOrWhiteSpace(model.DiscountCode)
                    ? null
                    : model.DiscountCode.Trim(),
                DeliveryAddress = model.Address,
                PhoneNumber = model.PhoneNumber
            };

            var order = await _orderService.CheckoutAsync(request);

            // Show confirmation (cash on delivery)
            model.OrderPlaced = true;
            model.OrderNumber = order.OrderNumber;
            model.FinalTotal = order.TotalAmount;
            model.Cart = new CartViewModel(); // clear display

            return View(model);
        }
    }
}
