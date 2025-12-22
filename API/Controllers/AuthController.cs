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

        [HttpPost("Authenticate")]
        public async Task<IActionResult> authenticate([FromBody] UserSignInRequest request)
        {
            var result = await _authService.AuthenticateAsync(request);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }
    }
}
