using Api.Models;
using Application.Interfaces;
using Application.Mappers;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> UpdateUser(UserDto userDto)
        {
            var userUpdate = UserMapper.ToEntity(userDto);

            if (userUpdate == null)
                return null;

            var user = await _userRepository.UpdateUser(userUpdate);

            if (user == null)
                return null;

            return UserMapper.ToDto(user);
        }
    }
}
