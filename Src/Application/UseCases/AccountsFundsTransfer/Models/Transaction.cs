namespace Application.UseCases.AccountsFundsTransfer.Models
{
    public class Transaction
    {
        public Guid? Id { get; set; }
        public int CorrelationId { get; set; }
        public DateTime DateTime { get; set; }
        public long ContaOrigem { get; set; }
        public long ContaDestino { get; set; }
        public decimal Valor { get; set; } //O valor mais correto para uso monetário é decimal e não uint/int
    }
}
