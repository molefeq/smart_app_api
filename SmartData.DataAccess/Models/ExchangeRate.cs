using System;

namespace SmartData.DataAccess.Models
{
    public partial class ExchangeRate
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreateDate { get; set; }
        public long? BaseCurrencyId { get; set; }

        public virtual Currency BaseCurrency { get; set; }

        public virtual Country Country { get; set; }
    }
}
