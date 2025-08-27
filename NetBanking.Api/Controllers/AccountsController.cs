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

        // Obtener una cuenta por ID De Cliente
        [HttpGet("{clientId}")]
        public async Task<ActionResult<Account>> GetAccount(int clientId)
        {
            var account = await _accountService.GetAccountsByClientIdAsync(clientId);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }



    }
}