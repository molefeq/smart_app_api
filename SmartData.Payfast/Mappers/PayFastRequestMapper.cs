namespace SmartData.Payfast.Mappers
{
    using SmartData.Payfast.Models;
    public class PayFastRequestMapper
    {
        public PayFastRequest MappToPayFastRequest(OnceOffPaymentModel onceOffPaymentModel)
        {
            var onceOffRequest = new PayFastRequest(onceOffPaymentModel.Merchant.PassPhrase);

            // Merchant Details
            onceOffRequest.merchant_id = onceOffPaymentModel.Merchant.MerchantId;
            onceOffRequest.merchant_key = onceOffPaymentModel.Merchant.MerchantKey;
            onceOffRequest.return_url = onceOffPaymentModel.Merchant.ReturnUrl;
            onceOffRequest.cancel_url = onceOffPaymentModel.Merchant.CancelUrl;
            onceOffRequest.notify_url = onceOffPaymentModel.Merchant.NotifyUrl;

            // Buyer Details
            onceOffRequest.email_address = onceOffPaymentModel.Buyer.EmailAddress;//  "sbtu01@payfast.co.za";
            onceOffRequest.name_first = onceOffPaymentModel.Buyer.Firstname;// "Testing";
            onceOffRequest.name_last = onceOffPaymentModel.Buyer.Lastname;//"User";

            // Transaction Details
            onceOffRequest.m_payment_id = onceOffPaymentModel.Transaction.PaymentId;// "8d00bf49-e979-4004-228c-08d452b86380";
            onceOffRequest.amount = onceOffPaymentModel.Transaction.Amount;//30;
            onceOffRequest.item_name = onceOffPaymentModel.Transaction.Item;//"Once off option";
            onceOffRequest.item_description = onceOffPaymentModel.Transaction.Description;//"Some details about the once off payment";

            // Transaction Options
            onceOffRequest.email_confirmation = onceOffPaymentModel.Transaction.IsConfirmationEmailProvided; // true;
            onceOffRequest.confirmation_address = onceOffPaymentModel.Transaction.ConfirmationAddress; // t"molefeq@gmail.com";

            return onceOffRequest;
        }
    }
}
