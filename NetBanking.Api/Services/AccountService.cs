using NetBanking.Api.Models;
using NetBanking.Api.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> GetAccountByIdAsync(int accountId)
    {
        return await _accountRepository.GetByIdAsync(accountId);
    }

    public async Task<IEnumerable<Account>> GetAllAccountsAsync()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId)
    {
        return await _accountRepository.GetAccountsByClientIdAsync(clientId);
    }

    public async Task AddAccountAsync(Account account)
    {
        await _accountRepository.AddAsync(account);
    }

    public async Task UpdateAccountAsync(Account account)
    {
        await _accountRepository.UpdateAsync(account);
    }

    public async Task DeleteAccountAsync(Account account)
    {
        await _accountRepository.DeleteAsync(account);
    }
}