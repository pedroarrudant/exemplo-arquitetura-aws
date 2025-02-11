using Application.Shared.Repositories.Interfaces;
using Application.UseCases.AccountsFundsTransfer.Models;
using Application.UseCases.AccountsFundsTransfer.Repository.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Application.UseCases.AccountsFundsTransfer.Repository
{
    [ExcludeFromCodeCoverage]
    public class AccountsFundsTransferRepository : IAccountsFundsTransferRepository
    {
        private readonly IBalanceRepository _repository;
        public AccountsFundsTransferRepository(IBalanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> CreateTransaction(int correlationId, long contaOrigem, long contaDestino, decimal valor)
        {
            try
            {
                var transaction = new Transaction()
                {
                    Id = Guid.NewGuid(),
                    ContaDestino = contaDestino,
                    ContaOrigem = contaOrigem,
                    Valor = valor,
                    DateTime = DateTime.Now,
                    CorrelationId = correlationId
                };

                //Salvaria a transação no banco de dados

                return transaction;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
