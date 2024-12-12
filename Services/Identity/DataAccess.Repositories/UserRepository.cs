using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<ApplicationUser?>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task<bool> CreateUserAsync(ApplicationUser? user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser? user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}