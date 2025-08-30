using Microsoft.AspNetCore.Mvc;
using NetBanking.Api.Models;
using NetBanking.Api.Requests;
using NetBanking.Api.Services;

namespace NetBanking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IAccountService _accountService;

        public ClientsController(IClientService clientService, IAccountService accountService)
        {
            _clientService = clientService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<Client>> GetClient(int clientId)
        {
            var client = await _clientService.GetClientByIdAsync(clientId);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient([FromBody] CreateClientRequest request)
        {
            // NUEVO: Hasheamos la contraseña antes de crear el objeto Client
            var newClient = new Client
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _clientService.AddClientAsync(newClient);
            return CreatedAtAction(nameof(GetClient), new { clientId = newClient.Id }, newClient);
        }

        [HttpPut("{clientId}")]
        public async Task<IActionResult> UpdateClient(int clientId, [FromBody] Client client)
        {
            if (clientId != client.Id)
            {
                return BadRequest("El ID del cliente en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            await _clientService.UpdateClientAsync(client);
            return NoContent();
        }

        [HttpDelete("{clientId}")]
        public async Task<IActionResult> DeleteClient(int clientId)
        {
            var client = await _clientService.GetClientByIdAsync(clientId);
            if (client == null)
            {
                return NotFound();
            }

            await _clientService.DeleteClientAsync(client);
            return NoContent();
        }

        /// <summary>
        /// Crea clientes y cuentas de prueba para el desarrollo.
        /// </summary>
        [HttpPost("seed")]
        public async Task<ActionResult> SeedClients()
        {
            // Usamos el mismo método de hashing para los clientes de prueba
            var client1 = new Client
            {
                Name = "Juan Pérez",
                Email = "juan@test.com",
                PhoneNumber = "123456789",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                Accounts = new List<Account>
                {
                    new Account { AccountNumber = "1001", Balance = 5000.00m },
                    new Account { AccountNumber = "1002", Balance = 2500.00m }
                }
            };

            var client2 = new Client
            {
                Name = "Ana García",
                Email = "ana@test.com",
                PhoneNumber = "987654321",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                Accounts = new List<Account>
                {
                    new Account { AccountNumber = "2001", Balance = 15000.00m },
                    new Account { AccountNumber = "2002", Balance = 5000.00m }
                }
            };

            await _clientService.AddClientAsync(client1);
            await _clientService.AddClientAsync(client2);

            return Ok("Clientes de prueba creados con éxito. Utiliza 'juan@test.com' o 'ana@test.com' con la contraseña 'password123' para iniciar sesión.");
        }
    }
}