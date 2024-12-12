using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
	public class AuthenticationController : ControllerBase
	{
		[HttpPost]
		[Route("/api/login")]
		public IActionResult Login(LoginRequest request)
		{
			return Ok(request);
		}
		[HttpPost]
		[Route("/api/logout")]
		public IActionResult Logout()
		{
			return Ok();
		}
	}
}
