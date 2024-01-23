using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace asset_loaning_api.models.domains
{
    public class transactions
    {
        [Key]
        public Guid transactionID { get; set; }

        public Guid supervisorId { get; set; }
        [ForeignKey("supervisorId")]
        public virtual UserDetails supervisor { get; set; } 


        public Guid studentID { get; set; }
        [ForeignKey("studentID")]
        public virtual UserDetails student { get; set; } 


        public Guid assetId { get; set; }
        [ForeignKey("assetId")]
        public virtual AssetDetails asset { get; set; }

        public string transaction_type { get; set; }
        public DateOnly transaction_date { get; set; }
        
        public Guid? returning_supervisorId { get; set; }
        [ForeignKey("returning_supervisorId")]
        public virtual UserDetails returning_supervisor { get; set; } = null!;




    }
}
