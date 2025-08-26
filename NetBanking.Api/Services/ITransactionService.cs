using NetBanking.Api.Models;

namespace NetBanking.Api.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    }
}