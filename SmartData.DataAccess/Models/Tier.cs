using System.Collections.Generic;

namespace SmartData.DataAccess.Models
{
    public partial class Tier
    {
        public Tier()
        {
            TopupOption = new HashSet<TopupOption>();
        }

        public int Id { get; set; }
        public string TierDescription { get; set; }

        public virtual ICollection<TopupOption> TopupOption { get; set; }
    }
}
