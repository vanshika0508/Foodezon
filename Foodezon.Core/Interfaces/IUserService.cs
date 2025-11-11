using Foodezon.Core.Models;

namespace Foodezon.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string firstName, string lastName, string email, string phoneNumber, string address);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> UpdateUserAsync(int id, string firstName, string lastName, string phoneNumber, string address);
        Task<bool> DeleteUserAsync(int id);
    }
}