using Application.Shared.Models;

namespace Application.UseCases.AccountsFundsTransfer.UseCase.Interfaces
{
    public interface IAccountsFundsTransferUseCaseHandler
    {
        Task<Result?> CreateTransaction(int correlationId, long contaOrigem, long contaDestino, decimal valor);
    }
}