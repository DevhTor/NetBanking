using Microsoft.AspNetCore.Mvc;
using NetBanking.Api.Requests;
using NetBanking.Api.Services;
using System.IdentityModel.Tokens.Jwt;

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
            var token = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas." });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var clientIdClaim = jwtSecurityToken?.Claims.FirstOrDefault(c => c.Type == "nameid");

            if (clientIdClaim == null)
            {
                return Unauthorized(new { message = "Error al obtener ID de cliente del token." });
            }

            return Ok(new { token, clientId = int.Parse(clientIdClaim.Value) });
        }
    }
}