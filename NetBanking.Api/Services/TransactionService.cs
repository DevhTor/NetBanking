using NetBanking.Api.Models;
using NetBanking.Api.Repositories;

namespace NetBanking.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;

        public TransactionService(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            var allTransactions = await _transactionRepository.GetAllAsync();
            return allTransactions.Where(t => t.SourceAccountId == accountId || t.DestinationAccountId == accountId);
        }
    }
}