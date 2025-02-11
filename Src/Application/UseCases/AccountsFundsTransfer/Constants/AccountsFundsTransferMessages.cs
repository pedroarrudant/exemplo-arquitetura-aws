namespace Application.UseCases.AccountsFundsTransfer.Constants
{
    public static class AccountsFundsTransferMessages
    {
        public static string NoFundsError = "Transacao foi cancelada por falta de saldo";//Não é correto passar o correlation como número de transação.
        public static string SuccessOperation = "Transacao numero {0} foi efetivada com sucesso! Novos saldos: Conta Origem: {1} | Conta Destino: {2}";
    }
}
