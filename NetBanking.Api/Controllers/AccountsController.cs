using Microsoft.AspNetCore.Mvc;
using NetBanking.Api.Models;
using NetBanking.Api.Repositories;
using NetBanking.Api.Requests; // Asegúrate de tener esta línea si moviste TransferRequest a una carpeta DTOs
using NetBanking.Api.Services;

namespace NetBanking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IRepository<Account> _accountRepository; // Puedes usar el repositorio directamente para las operaciones CRUD

        public AccountsController(IAccountService accountService, IRepository<Account> accountRepository)
        {
            _accountService = accountService;
            _accountRepository = accountRepository;
        }

        // Obtener una cuenta por ID
        [HttpGet("{accountId}")]
        public async Task<ActionResult<Account>> GetAccount(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // Obtener todas las cuentas (opcional, para propósitos de administración)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return Ok(accounts);
        }

        // Crear una nueva cuenta
        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] Account newAccount)
        {
            await _accountRepository.AddAsync(newAccount);
            return CreatedAtAction(nameof(GetAccount), new { accountId = newAccount.Id }, newAccount);
        }

        // Actualizar una cuenta existente
        [HttpPut("{accountId}")]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] Account updatedAccount)
        {
            if (accountId != updatedAccount.Id)
            {
                return BadRequest("El ID de la cuenta no coincide.");
            }

            var existingAccount = await _accountRepository.GetByIdAsync(accountId);
            if (existingAccount == null)
            {
                return NotFound();
            }

            await _accountRepository.UpdateAsync(updatedAccount);
            return NoContent();
        }

        // Eliminar una cuenta
        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            await _accountRepository.DeleteAsync(account);
            return NoContent();
        }

        // Requerimiento a y b: Visualizar cuentas y saldo de un cliente
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountsByClient(int clientId)
        {
            var accounts = await _accountService.GetAccountsByClientIdAsync(clientId);
            if (accounts == null || !accounts.Any())
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        // Requerimiento d: Hacer una transferencia
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            var success = await _accountService.TransferAsync(request.SourceAccountId, request.DestinationAccountId, request.Amount, request.Description);

            if (!success)
            {
                return BadRequest("Transferencia fallida. Verifique los datos, el saldo o la descripción.");
            }

            return Ok("Transferencia realizada con éxito.");
        }
    }
}