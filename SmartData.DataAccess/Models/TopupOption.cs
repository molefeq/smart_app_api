using System;

namespace SmartData.DataAccess.Models
{
    public partial class TopupOption
    {
        public long Id { get; set; }
        public int DataQuantity { get; set; }
        public string DataScale { get; set; }
        public string DataQuantityDescription { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public long? CurrencyId { get; set; }
        public int TierId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Tier Tier { get; set; }
    }
}
