using SmartData.Data.ViewModels;
using SmartData.DataAccess.Models;
using SmartData.Payfast.Models;

namespace SmartData.Service.DataMappers
{
    public class OnceOffPaymentMapper
    {
        public OnceOffPaymentModel MapToOnceOffPaymentModel(PaymentDetail paymentDetail, BuyDataModel buyDataModel, PayFastSettings payFastSettings)
        {
            return new OnceOffPaymentModel()
            {
                Merchant = payFastSettings,
                Buyer = new BuyerDetail
                {
                    Firstname=buyDataModel.Firstname,
                    Lastname = buyDataModel.Lastname,
                    EmailAddress = buyDataModel.EmailAddress
                },
                Transaction = new TransactionDetail
                {
                    PaymentId = paymentDetail.PaymentId.ToString(),
                    Amount = decimal.ToDouble(buyDataModel.Amount),
                    Item = "Airtime Top Up",
                    Description = $"Airtime Top Up For {buyDataModel.Firstname} { buyDataModel.Lastname}",
                    ConfirmationAddress = buyDataModel.EmailAddress,
                    IsConfirmationEmailProvided = true
                }
            };
        }
    }
}
