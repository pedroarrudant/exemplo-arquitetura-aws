using Application.UseCases.AccountsFundsTransfer.Models;
using Application.UseCases.GetAccountBalance.Models;

namespace Application.UseCases.AccountsFundsTransfer.Repository.Interfaces
{
    public interface IAccountsFundsTransferRepository
    {
        Task<Transaction> CreateTransaction(int correlationId, long contaOrigem, long contaDestino, decimal valor);
    }
}