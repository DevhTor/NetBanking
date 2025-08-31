using Microsoft.AspNetCore.Mvc;
using NetBanking.Api.Requests;
using NetBanking.Api.Services;

namespace NetBanking.Api.Controllers
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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var client = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (client == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas." });
            }

            var token = _authService.GenerateJwtToken(client);
            var clientId = client.Id;
            var clientName = client.Name;

            return Ok(new LoginResponse { Token = token, ClientId = clientId, ClientName = clientName });
        }
    }
}