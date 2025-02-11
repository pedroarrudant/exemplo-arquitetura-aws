using Application.Shared.Repositories.Interfaces;
using Application.UseCases.GetAccountBalance.Models;
using System.Diagnostics.CodeAnalysis;

namespace Application.Shared.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BalanceRepository : IBalanceRepository
    {
        private readonly List<AccountBalance> accountsBalanceList;
        public BalanceRepository()
        {
            //Coloquei os saldos das contas em memória para conseguir consultar o saldo novamente com ele atualizado.
            accountsBalanceList = new List<AccountBalance>();

            accountsBalanceList.Add(new AccountBalance(938485762, 180));
            accountsBalanceList.Add(new AccountBalance(347586970, 1200));
            accountsBalanceList.Add(new AccountBalance(2147483649, 0));
            accountsBalanceList.Add(new AccountBalance(675869708, 4900));
            accountsBalanceList.Add(new AccountBalance(238596054, 478));
            accountsBalanceList.Add(new AccountBalance(573659065, 787));
            accountsBalanceList.Add(new AccountBalance(210385733, 10));
            accountsBalanceList.Add(new AccountBalance(674038564, 400));
            accountsBalanceList.Add(new AccountBalance(563856300, 1200));
        }

        public async Task<AccountBalance?> GetAccountBalance(long accountId)
        {
            return accountsBalanceList.FirstOrDefault(x => x.Conta == accountId);
        }

        public async Task<AccountBalance?> UpdateBalance(long accountId, decimal newValue)
        {
            try
            {
                var accountBalance = accountsBalanceList.FirstOrDefault(x => x.Conta == accountId);

                if (accountBalance is not null)
                {
                    accountBalance.Saldo = newValue; 
                }

                return accountBalance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
