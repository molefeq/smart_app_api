using System;

namespace SmartData.Data.ViewModels
{
    public class BuyDataModel
    {
        public decimal Amount { get; set; }
        public long CurrencyId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public long UserId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
