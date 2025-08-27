// ITransactionService.cs
using NetBanking.Api.Models;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    Task CreateTransactionAsync(Transaction transaction);
}