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

        
    }
}