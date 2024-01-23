using System.ComponentModel.DataAnnotations;

namespace asset_loaning_api.models.domains
{
    public class AssetDetails
    {
        [Key]
        public Guid assetid { get; set; }
        public string name { get; set; }
        public string serialnumber { get; set; }
        public string model { get; set; }
        public int occupied { get; set; }
    }
}
