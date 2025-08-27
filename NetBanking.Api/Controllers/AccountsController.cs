using Microsoft.AspNetCore.Mvc;
using NetBanking.Api.Models;
using NetBanking.Api.Services;

namespace NetBanking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult<Account>> GetAccountById(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountsByClientId(int clientId)
        {
            var accounts = await _accountService.GetAccountsByClientIdAsync(clientId);

            if (accounts == null || !accounts.Any())
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] Account account)
        {
            await _accountService.AddAccountAsync(account);
            return CreatedAtAction(nameof(GetAccountById), new { accountId = account.Id }, account);
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] Account account)
        {
            if (accountId != account.Id)
            {
                return BadRequest("Account ID mismatch.");
            }

            var existingAccount = await _accountService.GetAccountByIdAsync(accountId);
            if (existingAccount == null)
            {
                return NotFound();
            }

            await _accountService.UpdateAccountAsync(account);
            return NoContent();
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            await _accountService.DeleteAccountAsync(account);
            return NoContent();
        }
    }
}