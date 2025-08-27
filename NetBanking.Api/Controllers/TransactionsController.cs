using Microsoft.AspNetCore.Mvc;
using NetBanking.Api.Models;
using NetBanking.Api.Requests; // Importa el namespace de TransferRequest

namespace NetBanking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(int accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId);

            if (transactions == null || !transactions.Any())
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult> TransferFunds([FromBody] TransferRequest request)
        {
            try
            {
                await _transactionService.TransferFundsAsync(request);
                return Ok(new { message = "Transferencia realizada con éxito." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}