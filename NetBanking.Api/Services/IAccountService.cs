using NetBanking.Api.Models;

namespace NetBanking.Api.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId);
        Task<bool> TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount, string description);
    }
}