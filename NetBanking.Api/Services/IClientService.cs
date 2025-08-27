using NetBanking.Api.Models;

namespace NetBanking.Api.Services
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int clientId);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);
    }
}