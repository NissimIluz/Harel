using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var isSuccess = await _authService.LoginAsync(dto.Username, dto.Password);

            if(isSuccess) 
                return Ok(new { message = "Login successful" });

            return Unauthorized(new { message = "Invalid credentials" });
        }
    }

}
