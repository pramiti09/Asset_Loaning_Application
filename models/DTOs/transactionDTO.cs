namespace asset_loaning_api.models.DTOs
{
    public class transactionDTO
    {

        public Guid requesting_userid {  get; set; }
        public string transaction_type { get; set; }
        public Guid? supervisorId { get; set; }
        public Guid? studentID { get; set; }
        public Guid? assetId { get; set; }
        public string? transaction_date { get; set; }
        
        public Guid? loan_transactionID { get; set; }
        public Guid? returning_supervisorId { get; set; }

    }
}
