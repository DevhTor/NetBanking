// AccountService.cs
using NetBanking.Api.Models;
using NetBanking.Api.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository; // Usar IAccountRepository


    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }


    public async Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId)
    {

        return await _accountRepository.GetAccountsByClientIdAsync(clientId);
    }

}