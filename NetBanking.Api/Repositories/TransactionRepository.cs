using Microsoft.EntityFrameworkCore;
using NetBanking.Api.Data;
using NetBanking.Api.Models;

namespace NetBanking.Api.Repositories
{
    public class TransactionRepository : IRepository<Transaction>, ITransactionRepository
    {
        private readonly NetbankingDbContext _context;

        public TransactionRepository(NetbankingDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        // Implementación del método específico
        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.Transactions
                                 .Where(t => t.SourceAccountId == accountId || t.DestinationAccountId == accountId)
                                 .ToListAsync();
        }
    }
}