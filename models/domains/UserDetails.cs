using System.ComponentModel.DataAnnotations;

namespace asset_loaning_api.models.domains
{
    public class UserDetails
    {
        [Key]
        public Guid userId {  get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string role { get; set; }

    }
}
