using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestIdentity.Models;
using TestIdentity.RabbitMq;

namespace TestIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRabbitMqService _rabbitMqService;

        public UsersController(IUserService userService, IRabbitMqService rabbitMqService)
        {
            _userService = userService;
            _rabbitMqService = rabbitMqService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(new UserModel() { Id = user.Id, UserName = user.UserName, Email = user.Email, Name = user.Name, IsFound = true});
        }

        [Route("GetUserByName")]
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUserByName(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);
            if (user == null)
                return NotFound();

            return Ok(new UserModel() { Id = user.Id, UserName = user.UserName, Email = user.Email, Name = user.Name, IsFound = true });
        }

        [Route("SendMessageAndGetUserByName")]
        [HttpGet]
        public async Task<ActionResult<UserModel>> SendMessageAndGetUserByName(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);
            if (user == null)
                return NotFound();

            var userModel = new UserModel() { Id = user.Id, UserName = user.UserName, Email = user.Email, Name = user.Name, IsFound = true };
            try
            {
                _rabbitMqService.SendMessage(userModel);
            }
            catch (Exception ex)
            {
                
            }

            return Ok(userModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(string id, [FromBody] UserDto userDto)
        {
            var result = await _userService.UpdateUserAsync(userDto);
            if (!result)
                return BadRequest("Failed to update user");

            var userModel = new UserModel() { Id = Guid.Parse(id), UserName = userDto.UserName, Email = userDto.Email, Name = userDto.Name, IsFound = true };
            try
            {
                _rabbitMqService.SendMessage(userModel);
            }
            catch (Exception ex)
            {

            }

            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return BadRequest("Failed to delete user");

            var userModel = new UserModel() { Id = Guid.Parse(id), IsDeleted = true };
            try
            {
                _rabbitMqService.SendMessage(userModel);
            }
            catch (Exception ex)
            {

            }

            return Ok("User deleted successfully");
        }
    }
}