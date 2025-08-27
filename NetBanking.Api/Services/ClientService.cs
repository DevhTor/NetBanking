// ClientService.cs
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

}