using Application.UseCases.AccountsFundsTransfer.Models;

namespace TransacaoFinanceiraTests.Static
{
    public static class TransactionStatic
    {
        public static Transaction GetASingleTransaction => 
            new Transaction() { 
                                CorrelationId = 1, 
                                ContaOrigem = 1, 
                                ContaDestino = 2, 
                                DateTime = DateTime.Now, 
                                Id = Guid.NewGuid(), 
                                Valor = 1000 };
                               }
}
