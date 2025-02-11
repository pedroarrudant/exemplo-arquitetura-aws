using Application.UseCases.GetAccountBalance.Models;

namespace TransacaoFinanceiraTests.Static
{
    public class AccountBalanceStatic
    {
        public static AccountBalance GetAOriginAccountBalance => new AccountBalance(1, 1000);

        public static AccountBalance GetATargetAccountBalance => new AccountBalance(2, 1000);

        public static List<AccountBalance> GetAListOfAccountBalances => 
            new List<AccountBalance>() { 
                                        new AccountBalance(1, 1500),
                                        new AccountBalance(2, 1000),
                                        new AccountBalance(4, 50),
                                        new AccountBalance(5, 10),
                                        new AccountBalance(6, 0)
                                        };
    }
}
