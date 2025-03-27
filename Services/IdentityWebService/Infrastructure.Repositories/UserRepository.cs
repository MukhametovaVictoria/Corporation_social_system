using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="user">User info</param>
        /// <param name="register">Register info</param>
        /// <returns>Object: IdentityResult</returns>
        public async Task<IdentityResult> RegisterUser(User user, Register register)
        {
            await _context.Users.AddAsync(user);

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                var emailDomainParts = register.Email.Split('@');
                var emailDomain = emailDomainParts[emailDomainParts.Length - 1];
                if (await _context.EmailDomains.AnyAsync(ed => ed.Domain == emailDomain))
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                }
                await _userManager.AddToRoleAsync(user, "User");
            }

            await _context.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// Logging
        /// </summary>
        /// <param name="username">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Object: User</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<User> LoginUser(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new UnauthorizedAccessException("A user is not found or email is not confirmed.");
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (!result)
            {
                throw new UnauthorizedAccessException("Wrong data.");
            }

            return user;
        }

        /// <summary>
        /// Email confirmation
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="token">Token</param>
        /// <returns>Success: True/False</returns>
        public async Task<User?> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                    return user;
            }

            return null;
        }

        public async Task<bool> ChangeEmailAsync(string token, string oldEmail, string newEmail)
        {
            var user = await _userManager.FindByEmailAsync(oldEmail);
            if (user != null)
            {
                var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
                return result.Succeeded;
            }

            return false;
        }

        /// <summary>
        /// Password changing
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="newPassword">New password</param>
        /// <param name="oldPassword">Old password</param>
        /// <returns>Success: True/False</returns>
        public async Task<bool> ChangePasswordAsync(string userId, string newPassword, string oldPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

                return result.Succeeded;
            }

            return false;
        }

        /// <summary>
        /// Token refreshing
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>Object: User</returns>
        public async Task<User?> RefreshToken(Token token)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == token.RefreshToken);
        }

        /// <summary>
        /// Email confirmation token generation
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Token</returns>
        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        /// <summary>
        /// Email change token generation
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="newEmail">New email</param>
        /// <returns>Token</returns>
        public async Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
        {
            return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public async Task<User?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        /// <summary>
        /// User updating
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>Object: User</returns>
        public async Task<User?> UpdateUser(User user)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if(userDb != null)
            {
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Patronymic = user.Patronymic;
                userDb.DateOfBirth = user.DateOfBirth;

                await _userManager.UpdateAsync(userDb);

                await _context.SaveChangesAsync();

                return userDb;
            }

            return null;
        }
    }
}
