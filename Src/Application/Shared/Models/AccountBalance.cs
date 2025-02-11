namespace Application.UseCases.GetAccountBalance.Models
{
    public class AccountBalance
    {
        //Os nomes foram mantidos em pt-BR por conta da fidelidade com o exercicio
        public long Conta { get; set; }
        public decimal Saldo { get; set; }

        public AccountBalance(long conta, decimal saldo)
        {
            this.Saldo = saldo;
            this.Conta = conta;
        }
    }
}
