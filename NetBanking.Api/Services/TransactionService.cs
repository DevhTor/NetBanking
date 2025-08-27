using NetBanking.Api.Models;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        // Delega la búsqueda al repositorio, que lo hará de forma eficiente.
        return await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
    }

    public async Task CreateTransactionAsync(Transaction transaction)
    {
        // Delega la creación de la transacción al repositorio.
        await _transactionRepository.AddAsync(transaction);
    }
}