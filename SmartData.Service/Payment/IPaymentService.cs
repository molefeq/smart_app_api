using SmartData.Data.ViewModels;
using SmartData.Payfast.Models;
using System;

namespace SmartData.Service.Payment
{
    public interface IPaymentService
    {
        OnceOffPaymentResponse GetOnceOffPaymentUrl(BuyDataModel buyDataModel, PayFastSettings payFastSettings);

        void FailOnceOffPayment(Guid paymentId);
    }
}
