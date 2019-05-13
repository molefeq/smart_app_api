using System;

namespace SmartData.DataAccess.Models
{
    public partial class PaymentDetail
    {
        public long Id { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public long CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsPaymentSuccessful { get; set; }
        public long CurrencyId { get; set; }

        public virtual Account CreateUser { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
