using NetBanking.Api.Models;
using NetBanking.Api.Requests;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    Task CreateTransactionAsync(Transaction transaction);
    Task TransferFundsAsync(TransferRequest request);
}