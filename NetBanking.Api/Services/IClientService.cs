using NetBanking.Api.Models;

namespace NetBanking.Api.Services
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int clientId);
        Task<IEnumerable<Account>> GetClientAccountsAsync(int clientId);
    }
}