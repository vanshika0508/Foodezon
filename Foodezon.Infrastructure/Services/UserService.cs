using Foodezon.Core.Interfaces;
using Foodezon.Core.Models;

namespace Foodezon.Infrastructure.Services
{
    public class UserService : IUserService

    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<User> CreateUserAsync(string firstName, string lastName, string email, string phoneNumber, string address)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateUserAsync(int id, string firstName, string lastName, string phoneNumber, string address)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IUserRepository
    {
    }
}