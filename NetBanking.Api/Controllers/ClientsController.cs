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

    }
}