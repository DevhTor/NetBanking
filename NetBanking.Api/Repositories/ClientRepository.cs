using Microsoft.EntityFrameworkCore;
using NetBanking.Api.Data;
using NetBanking.Api.Models;

namespace NetBanking.Api.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly NetbankingDbContext _context;

        public ClientRepository(NetbankingDbContext context)
        {
            _context = context;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}