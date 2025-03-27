using Api.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto? ToDto(User user)
        {
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

        public static User? ToEntity(UserDto userDto)
        {
            if (userDto == null)
                return null;

            return new User
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Patronymic = userDto.Patronymic,
                DateOfBirth = userDto.DateOfBirth
            };
        }
    }
}