using NetBanking.Api.Models;
using NetBanking.Api.Repositories;

namespace NetBanking.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public AccountService(IRepository<Account> accountRepository, IRepository<Transaction> transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Account>> GetAccountsByClientIdAsync(int clientId)
        {
            // Lógica para obtener las cuentas de un cliente específico.
            // Es más eficiente buscar directamente en el repositorio de clientes si lo hubieras implementado.
            var allAccounts = await _accountRepository.GetAllAsync();
            return allAccounts.Where(a => a.ClientId == clientId);
        }

        public async Task<bool> TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount, string description)
        {
            var sourceAccount = await _accountRepository.GetByIdAsync(sourceAccountId);
            var destinationAccount = await _accountRepository.GetByIdAsync(destinationAccountId);

            if (sourceAccount == null || destinationAccount == null)
            {
                return false; // Una de las cuentas no existe.
            }

            if (sourceAccount.Balance < amount)
            {
                return false; // Saldo insuficiente.
            }

            // Realiza la transferencia y guarda los cambios en la base de datos.
            sourceAccount.Balance -= amount;
            destinationAccount.Balance += amount;

            await _accountRepository.UpdateAsync(sourceAccount);
            await _accountRepository.UpdateAsync(destinationAccount);

            // Crea un registro de la transacción.
            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                SourceAccountId = sourceAccountId,
                DestinationAccountId = destinationAccountId,
                Description = description // Usa la descripción proporcionada por el usuario.
            };

            await _transactionRepository.AddAsync(transaction);

            return true;
        }
    }
}