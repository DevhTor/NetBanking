using NetBanking.Api.Models;
using NetBanking.Api.Repositories;
using NetBanking.Api.Services;

public class ClientService : IClientService
{
    private readonly IRepository<Client> _clientRepository;

    public ClientService(IRepository<Client> clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client> GetClientByIdAsync(int clientId)
    {
        return await _clientRepository.GetByIdAsync(clientId);
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    public async Task AddClientAsync(Client client)
    {
        await _clientRepository.AddAsync(client);
    }

    public async Task UpdateClientAsync(Client client)
    {
        await _clientRepository.UpdateAsync(client);
    }

    public async Task DeleteClientAsync(Client client)
    {
        await _clientRepository.DeleteAsync(client);
    }
}