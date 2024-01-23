namespace asset_loaning_api.models.DTOs
{
    public class GetTransactionDTO
    {
        public Guid transactionId { get; set; }
        public String? StudentName { get; set; }
        public String? Loaning_Supervisor { get; set; }
        public String? Asset { get; set; }
        public String? transaction_date { get; set; }
        
        public String? transaction_type { get; set; }
        public String? Returning_Supervisor { get; set; }

    }
}
