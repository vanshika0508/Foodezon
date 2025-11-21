using Foodezon.Core.Models;
using System.Net.Http.Json;

namespace Foodezon.Web.Services
{
    public class AdminApiService
    {
        private readonly HttpClient _http;
        public AdminApiService(HttpClient http) => _http = http;

        // Dishes

        public async Task<List<Dish>> GetDishesAsync() => 
            await _http.GetFromJsonAsync<List<Dish>>("api/admin/dishes");

        public async Task<Dish?> GetDishAsync(int id) =>
            await _http.GetFromJsonAsync<Dish>($"api/admin/dish/{id}");

        public async Task CreateDishAsync (Dish dish)
        {
            var resp = await _http.PostAsJsonAsync("api/admin/dish", dish);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateDishAsync(int id, Dish dish)
        {
            var resp = await _http.PostAsJsonAsync($"api/admin/dish/{id}", dish);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteDishAsync (int id)
        {
            var resp = await _http.DeleteAsync($"api/admin/dish/{id}");
            resp.EnsureSuccessStatusCode();
        }

        // Discounts

        public async Task<List<Discount>> GetDiscountsForDishAsync(int dishId) =>
            await _http.GetFromJsonAsync<List<Discount>>($"api/admin/dish/{dishId}/discounts");

        public async Task<List<Discount>> GetActiveDiscountsForDishAsync(int dishId) =>
            await _http.GetFromJsonAsync<List<Discount>>($"api/admin/dish/{dishId}/discounts/active");

        public async Task AddDiscountAsync (int dishId, Discount disc)
        {
            var resp = await _http.PostAsJsonAsync($"api/admin/dish/{dishId}/discount", disc);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteDiscountAsync (int discountId)
        {
            var resp = await _http.DeleteAsync($"api/admin/discount/{discountId}");
            resp.EnsureSuccessStatusCode();
        }
        
        // Orders

        public async Task<List<Order>> GetOrdersAsync() =>
            await _http.GetFromJsonAsync<List<Order>>("api/admin/orders");

        public async Task<List<Order>> GetOrderAsync(int orderId) =>
            await _http.GetFromJsonAsync<Order>("api/admin/orders/{orderId}");

        public async Task CreateOrderAsync (Order order)
        {
            var resp = await _http.PutAsJsonAsync($"api/admin/order", order);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateOrderAsync (int orderId, Order order)
        {
            var resp = await _http.PutAsJsonAsync($"api/admin/order/{orderId}", order);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrderAsync (int orderId)
        {
            var resp = await _http.DeleteAsync($"api/admin/order/{orderId}");
            resp.EnsureSuccessStatusCode();
        }
    }
}