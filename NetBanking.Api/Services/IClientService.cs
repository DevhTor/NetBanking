using NetBanking.Api.Models;

namespace NetBanking.Api.Services
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int clientId);
    }
}