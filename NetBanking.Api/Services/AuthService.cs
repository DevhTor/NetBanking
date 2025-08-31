using Microsoft.IdentityModel.Tokens;
using NetBanking.Api.Models;
using NetBanking.Api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetBanking.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<Client> clientRepository, IConfiguration configuration)
        {
            _clientRepository = clientRepository;
            _configuration = configuration;
        }

        public async Task<Client> AuthenticateAsync(string username, string password)
        {
            var client = (await _clientRepository.GetAllAsync()).FirstOrDefault(c => c.Email == username);

            if (client == null || !BCrypt.Net.BCrypt.Verify(password, client.PasswordHash))
            {
                return null;
            }

            return client;
        }

        public string GenerateJwtToken(Client client)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, client.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}