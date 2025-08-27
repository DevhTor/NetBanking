using NetBanking.Api.Models;

namespace NetBanking.Api.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId);

    }
}