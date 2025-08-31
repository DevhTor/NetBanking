// TransactionService.cs

using NetBanking.Api.Models;
using NetBanking.Api.Repositories;
using NetBanking.Api.Requests;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IRepository<Account> _accountRepository; // Añadir el repositorio de cuentas

    public TransactionService(ITransactionRepository transactionRepository, IRepository<Account> accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        return await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
    }

    public async Task CreateTransactionAsync(Transaction transaction)
    {
        await _transactionRepository.AddAsync(transaction);
    }

    public async Task TransferFundsAsync(TransferRequest request)
    {
        // Obtener la cuenta de origen
        var sourceAccount = await _accountRepository.GetByIdAsync(request.SourceAccountId);

        // MODIFICACIÓN: Buscar la cuenta de destino por el NÚMERO DE CUENTA
        var destinationAccount = (await _accountRepository.GetAllAsync()).FirstOrDefault(a => a.AccountNumber == request.DestinationAccountNumber);

        // Validar las cuentas y el saldo
        if (sourceAccount == null || destinationAccount == null)
        {
            throw new InvalidOperationException("Una o ambas cuentas no existen.");
        }

        if (sourceAccount.Balance < request.Amount)
        {
            throw new InvalidOperationException("Fondos insuficientes en la cuenta de origen.");
        }

        // Realizar la transferencia
        sourceAccount.Balance -= request.Amount;
        destinationAccount.Balance += request.Amount;

        // Actualizar los saldos en la base de datos
        await _accountRepository.UpdateAsync(sourceAccount);
        await _accountRepository.UpdateAsync(destinationAccount);

        // Crear y guardar las transacciones de débito y crédito
        var debitTransaction = new Transaction
        {
            Amount = request.Amount,
            TransactionDate = DateTime.UtcNow,
            Description = request.Description,
            SourceAccountId = request.SourceAccountId,
            DestinationAccountId = destinationAccount.Id // Usa el ID de la cuenta de destino encontrada
        };

        var creditTransaction = new Transaction
        {
            Amount = request.Amount,
            TransactionDate = DateTime.UtcNow,
            Description = request.Description,
            SourceAccountId = request.SourceAccountId,
            DestinationAccountId = destinationAccount.Id
        };

        await _transactionRepository.AddAsync(debitTransaction);
        await _transactionRepository.AddAsync(creditTransaction);
    }
}