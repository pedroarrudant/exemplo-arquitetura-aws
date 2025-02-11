using Application.Shared.Models;
using Application.Shared.Repositories.Interfaces;
using Application.UseCases.AccountsFundsTransfer.Constants;
using Application.UseCases.AccountsFundsTransfer.Repository.Interfaces;
using Application.UseCases.AccountsFundsTransfer.UseCase.Interfaces;

namespace Application.UseCases.AccountsFundsTransfer.UseCase
{
    public class AccountsFundsTransferUseCaseHandler : IAccountsFundsTransferUseCaseHandler
    {
        private readonly IAccountsFundsTransferRepository _transferRepository;
        private readonly IBalanceRepository _balanceRepository;
        public AccountsFundsTransferUseCaseHandler(IAccountsFundsTransferRepository repository, IBalanceRepository balanceRepository)
        {
            _transferRepository = repository;
            _balanceRepository = balanceRepository;
        }

        public async Task<Result?> CreateTransaction(int correlationId, long originAccount, long targetAccount, decimal valor)
        {
            var originAccountBalance = await _balanceRepository.GetAccountBalance(originAccount);
            var targetAccountBalance = await _balanceRepository.GetAccountBalance(targetAccount);

            if (originAccountBalance is not null && originAccountBalance.Saldo < valor)
            {
                return new Result() { 
                    Message = String.Format(AccountsFundsTransferMessages.NoFundsError), 
                    Code = nameof(AccountsFundsTransferMessages.NoFundsError) 
                };
            }

            var transaction = await _transferRepository.CreateTransaction(correlationId, originAccount, targetAccount, valor);

            if (originAccountBalance is not null)
            {
                var newOriginAccountBalance = originAccountBalance.Saldo - transaction.Valor;
                await _balanceRepository.UpdateBalance(originAccount, newOriginAccountBalance); 
            }

            if (targetAccountBalance is not null)
            {
                var newTargetAccountBalance = targetAccountBalance.Saldo + transaction.Valor;
                await _balanceRepository.UpdateBalance(targetAccount, newTargetAccountBalance); 
            }

            var newBalanceOriginAccount = await _balanceRepository.GetAccountBalance(originAccount);
            var newBalanceTargetAccount = await _balanceRepository.GetAccountBalance(targetAccount);

            if (transaction is not null)
            {
                return new Result() { 
                    Message = String.Format(
                            AccountsFundsTransferMessages.SuccessOperation, 
                            transaction.Id,
                            newBalanceOriginAccount?.Saldo,
                            newBalanceTargetAccount?.Saldo), 
                        Code = nameof(AccountsFundsTransferMessages.SuccessOperation) 
                };
            }

            return null;
        }
    }
}
