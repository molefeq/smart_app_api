using SmartData.Data.ViewModels;
using SmartData.Payfast.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartData.Service.Payment
{
    public interface IPaymentService
    {
        string GetOnceOffPaymentUrl(BuyDataModel buyDataModel, PayFastSettings payFastSettings);
    }
}
