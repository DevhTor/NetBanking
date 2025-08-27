// IAccountRepository.cs
using NetBanking.Api.Models;
using NetBanking.Api.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    // Método para obtener cuentas por ClientId de forma eficiente
    Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId);
}