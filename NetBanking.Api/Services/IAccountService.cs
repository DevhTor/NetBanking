using NetBanking.Api.Models;

namespace NetBanking.Api.Services
{
    public interface IAccountService
    {
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
    }
}