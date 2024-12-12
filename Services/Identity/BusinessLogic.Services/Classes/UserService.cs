using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user == null ? null : MapToUserDto(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(MapToUserDto);
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            var user = MapToApplicationUser(userDto);
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        private static UserDto MapToUserDto(ApplicationUser user)
        {
            return new UserDto
            {
                Id = new Guid(user.Id),
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name
            };
        }

        private static ApplicationUser MapToApplicationUser(UserDto userDto)
        {
            return new ApplicationUser
            {
                Id = userDto.Id.ToString(),
                UserName = userDto.UserName,
                Email = userDto.Email,
                Name = userDto.Name
            };
        }

        public async Task<UserDto?> GetUserByNameAsync(string userName)
        {
            var user = await _userRepository.GetUserByNameAsync(userName);
            return user == null ? null : MapToUserDto(user);
        }
    }
}