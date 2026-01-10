using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VendingMachines.Application.IServices;
using VendingMachines.Shared.DTOs.User;

namespace VendingMachines.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }
    }
}
