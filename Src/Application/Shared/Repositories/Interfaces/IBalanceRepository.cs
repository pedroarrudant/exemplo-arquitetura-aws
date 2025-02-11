using Application.UseCases.GetAccountBalance.Models;

namespace Application.Shared.Repositories.Interfaces
{
    public interface IBalanceRepository
    {
        Task<AccountBalance?> GetAccountBalance(long accountId);
        Task<AccountBalance?> UpdateBalance(long accountId, decimal newValue);
    }
}