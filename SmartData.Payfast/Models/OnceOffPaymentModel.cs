namespace SmartData.Payfast.Models
{
    public  class OnceOffPaymentModel
    {
        public PayFastSettings Merchant { get; set; }
        public BuyerDetail Buyer { get; set; }
        public TransactionDetail Transaction { get; set; }
    }
}
