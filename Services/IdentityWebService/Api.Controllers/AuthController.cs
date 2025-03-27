using Api.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IConfiguration configuration, IRabbitMqService rabbitMqService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _configuration = configuration;
            _rabbitMqService = rabbitMqService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterUser(registerDto);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok("The user is registered. Check your email and confirm.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var userDto = await _authService.LoginUser(loginDto.Username, loginDto.Password);
                
                var jwtToken = GenerateJwtToken(userDto);
                
                if(string.IsNullOrEmpty(jwtToken))
                    BadRequest("JWT Key is empty!");

                var refreshToken = GenerateRefreshToken();

                userDto.RefreshToken = refreshToken;
                userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

                return Ok(new { Token = jwtToken, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                var userDto = await _authService.ConfirmEmail(email, token);
                if (userDto != null)
                {
                    var host = _configuration["RabbitMq:Host"];
                    if (host != null)
                    {
                        var queue = _configuration[$"RabbitMq:{host}:Queue"];
                        if (queue != null)
                            _rabbitMqService.SendMessage(userDto, host, queue);
                    }
                    return Ok(userDto);
                }

                return BadRequest("Fail while email confirmation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("send-email-change-link")]
        public async Task<IActionResult> SendEmailChangeLink(string token, string email)
        {
            try
            {
                var result = await _authService.SendEmailChangeMessageAsync(email, token);
                if (result)
                {
                    return Ok(result);
                }

                return BadRequest("Fail while email confirmation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("change-email")]
        public async Task<IActionResult> ChangeEmail(string token, string newEmail, string oldEmail)
        {
            try { 
                var result = await _authService.ChangeEmailAsync(token, newEmail, oldEmail);
                if (result)
                {
                    return Ok(result);
                }

                return BadRequest("Fail while email confirmation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try { 
                var result = await _authService.ChangePasswordAsync(changePasswordDto.UserId, changePasswordDto.NewPassword, changePasswordDto.OldPassword);
            
                if(result)
                    return Ok("The password is changed.");

                return BadRequest("Fail while password changing.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        {
            try
            {
                var userDto = await _authService.RefreshToken(tokenDto);

                if (userDto == null || userDto.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    return Unauthorized();
                }

                var newJwtToken = GenerateJwtToken(userDto);
                var newRefreshToken = GenerateRefreshToken();

                userDto.RefreshToken = newRefreshToken;
                userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

                return Ok(new { Token = newJwtToken, RefreshToken = newRefreshToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private string? GenerateJwtToken(UserDto userDto)
        {
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "UserInfo"),
                    new Claim(JwtRegisteredClaimNames.NameId, userDto.Id),
                    new Claim(JwtRegisteredClaimNames.GivenName, userDto.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, userDto.LastName),
                    new Claim(JwtRegisteredClaimNames.Birthdate, userDto.DateOfBirth.ToString("s")+"Z"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var jwtKey = _configuration["Jwt:Key"];
            
            if (String.IsNullOrEmpty(jwtKey))
                return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddMinutes(15);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}