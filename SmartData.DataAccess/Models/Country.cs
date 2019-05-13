namespace SmartData.DataAccess.Models
{
    public partial class Country
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsUcloudEnabled { get; set; }
        public int? TierId { get; set; }
        public long? CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Tier Tier { get; set; }
    }
}
