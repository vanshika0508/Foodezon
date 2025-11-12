
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

       
        public async Task<User> CreateUserAsync(string firstName, string lastName, string email, string phoneNumber, string address)
        {
           
            var existing = await _userRepository.GetByEmailAsync(email.Trim().ToLower());
            if (existing != null)
                throw new Exception("A user with this email already exists.");

            var user = new User
            {
                FirstName   = firstName.Trim(),
                LastName    = lastName.Trim(),
                Email       = email.Trim().ToLower(),
                PhoneNumber = phoneNumber.Trim(),
                Address     = address.Trim(),
                Role        = UserRole.Customer
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

        
        public Task<User?> GetUserByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return _userRepository.GetAllAsync();
        }

    
        public async Task<User?> UpdateUserAsync(int id, string firstName, string lastName, string phoneNumber, string address)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            user.FirstName   = firstName.Trim();
            user.LastName    = lastName.Trim();
            user.PhoneNumber = phoneNumber.Trim();
            user.Address     = address.Trim();
            user.UpdatedAt   = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

       
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
