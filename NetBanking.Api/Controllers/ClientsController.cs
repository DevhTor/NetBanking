using Microsoft.AspNetCore.Mvc;
using NetBanking.Api.Models;
using NetBanking.Api.Services;

namespace NetBanking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
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
        public async Task<ActionResult<Client>> CreateClient([FromBody] Client client)
        {
            await _clientService.AddClientAsync(client);
            return CreatedAtAction(nameof(GetClient), new { clientId = client.Id }, client);
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
    }
}