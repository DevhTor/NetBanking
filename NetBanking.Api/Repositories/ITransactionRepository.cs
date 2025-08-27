// ITransactionRepository.cs
using NetBanking.Api.Models;
using NetBanking.Api.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    // Método para obtener transacciones por AccountId de forma eficiente
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
}