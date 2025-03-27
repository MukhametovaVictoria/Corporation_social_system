using Api.Models;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IEmailService emailService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _configuration = configuration;
        }

        /// <summary>
        /// User registartion
        /// </summary>
        /// <param name="registerDto">Registration data</param>
        /// <returns>Object: IdentityResult</returns>
        public async Task<IdentityResult> RegisterUser(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Patronymic = registerDto.Patronymic,
                DateOfBirth = registerDto.DateOfBirth
            };

            var register = new Register 
            { 
                Email = registerDto.Email, 
                Password = registerDto.Password, 
                UserName = registerDto.UserName 
            };
            var link = _configuration["EmailSettings:ConfirmationLink"];
            var siteUrl = _configuration["SysSettings:SiteUrl"];
            var result = await _userRepository.RegisterUser(user, register);
            var token = await _userRepository.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{siteUrl}{link}?token={token}&email={registerDto.Email}";
            await _emailService.SendEmailAsync(registerDto.Email, "Email confirmation", confirmationLink);

            return result;
        }

        /// <summary>
        /// User logging
        /// </summary>
        /// <param name="username">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Object: User</returns>
        public async Task<UserDto> LoginUser(string username, string password)
        {
            var user = await _userRepository.LoginUser(username, password);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                DateOfBirth = user.DateOfBirth
            };
        }

        /// <summary>
        /// Email confirmation
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="token">Token</param>
        /// <returns>Success: True/False</returns>
        public async Task<UserDto?> ConfirmEmail(string email, string token)
        {
            var user = await _userRepository.ConfirmEmail(email, token);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                DateOfBirth = user.DateOfBirth
            };
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
            return await _userRepository.ChangePasswordAsync(userId, newPassword, oldPassword);
        }

        /// <summary>
        /// Email changing
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="newEmail">New email</param>
        /// <param name="oldEmail">Old email</param>
        /// <returns>Success: True/False</returns>
        public async Task<bool> ChangeEmailAsync(string token, string newEmail, string oldEmail)
        {
            return await _userRepository.ChangeEmailAsync(token, oldEmail, newEmail);
        }

        /// <summary>
        /// Send email change link
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="newEmail">New email</param>
        /// <returns>Success: True/False</returns>
        public async Task<bool> SendEmailChangeMessageAsync(string userId, string newEmail)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return false;
            var token = await _userRepository.GenerateChangeEmailTokenAsync(user, newEmail);
            var link = _configuration["EmailSettings:ChangeEmailLink"];
            var siteUrl = _configuration["SysSettings:SiteUrl"];
            var confirmationLink = $"{siteUrl}{link}?token={token}&newEmail={newEmail}&oldEmail={user.Email}";
            await _emailService.SendEmailAsync(newEmail, "Email confirmation", confirmationLink);

            return true;
        }

        /// <summary>
        /// Token refreshing
        /// </summary>
        /// <param name="tokenDto">Token</param>
        /// <returns>Object: User</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<UserDto> RefreshToken(TokenDto tokenDto)
        {
            var user = await _userRepository.RefreshToken(new Token() { RefreshToken = tokenDto.RefreshToken});

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("A user is not found or refresh token is expired.");
            }

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                DateOfBirth = user.DateOfBirth
            };
        }
    }
}