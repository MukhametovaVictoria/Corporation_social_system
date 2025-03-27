using Api.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> UpdateUser(UserDto userDto);
    }
}
