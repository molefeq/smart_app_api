using SmartData.Data.ViewModels;
using SmartData.DataAccess.Models;
using System;

namespace SmartData.Service.DataMappers
{
    public class PaymentDetailMapper
    {
        public PaymentDetail MapToPaymentDetail(BuyDataModel buyDataModel)
        {
            return new PaymentDetail()
            {
                PaymentId = Guid.NewGuid(),
                Amount = buyDataModel.Amount,
                CurrencyId = buyDataModel.CurrencyId,
                CreateUserId = buyDataModel.UserId,
                IsPaymentSuccessful = false,
                CreateDate = DateTime.Now
            };
        }
    }
}
