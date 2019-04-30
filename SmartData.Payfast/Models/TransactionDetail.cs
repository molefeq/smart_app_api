namespace SmartData.Payfast.Models
{
    public class TransactionDetail
    {
        public string PaymentId { get; set; }
        public double Amount { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public bool IsConfirmationEmailProvided { get; set; }
        public string ConfirmationAddress { get; set; }
    }
}
