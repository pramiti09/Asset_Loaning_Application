namespace asset_loaning_api.models.domains
{
    public class GetTransaction
    {
        public Guid transactionId {  get; set; }
        public Guid studentId { get; set; }
        public string StudentName { get; set; }
        public Guid supervisorId { get; set; }
        public string Loaning_Supervisor { get; set; }
        
        public Guid assetId { get; set; }
        public string Asset { get; set;}
        public DateOnly? transaction_date { get; set; }
          
        public string transaction_type { get; set; }
        public Guid? returning_supervisorId { get; set; }
        public string Returning_Supervisor { get; set; }
    }
}
