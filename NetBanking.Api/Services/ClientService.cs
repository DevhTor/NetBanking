using NetBanking.Api.Models;
using NetBanking.Api.Repositories;

namespace NetBanking.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Account> _accountRepository;

        public ClientService(IRepository<Client> clientRepository, IRepository<Account> accountRepository)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _clientRepository.GetByIdAsync(clientId);
        }

        public async Task<IEnumerable<Account>> GetClientAccountsAsync(int clientId)
        {
            var accounts = await _accountRepository.GetAllAsync();
            return accounts.Where(a => a.ClientId == clientId);
        }
    }
}