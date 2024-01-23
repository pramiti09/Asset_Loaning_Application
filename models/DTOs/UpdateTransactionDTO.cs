using asset_loaning_api.models.domains;

namespace asset_loaning_api.models.DTOs
{
    public class UpdateTransactionDTO
    {
        public Guid requesting_userid { get; set; }
        public Guid transactionID { get; set; }
        public Guid? supervisorId { get; set; }
        
        public Guid? studentID { get; set; }
        
        public Guid? assetId { get; set; }
        
        public string? transaction_type { get; set; }
        public string? transaction_date { get; set; }
        
        public Guid? returning_supervisorId { get; set; }
    }
}
