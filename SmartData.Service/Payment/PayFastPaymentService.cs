using SmartData.Data.ViewModels;
using SmartData.Payfast;
using SmartData.Payfast.Models;
using SmartData.Service.DataMappers;

namespace SmartData.Service.Payment
{
    public class PayFastPaymentService : IPaymentService
    {
        private IPayFastService payFastService;
        private OnceOffPaymentMapper onceOffPaymentMapper;

        public PayFastPaymentService(IPayFastService payFastService, OnceOffPaymentMapper onceOffPaymentMapper)
        {
            this.payFastService = payFastService;
            this.onceOffPaymentMapper = onceOffPaymentMapper;
        }

        public string GetOnceOffPaymentUrl(BuyDataModel buyDataModel, PayFastSettings payFastSettings)
        {
            var onceOffPaymentModel = onceOffPaymentMapper.MapToOnceOffPaymentModel(buyDataModel, payFastSettings);

            return payFastService.CreateOncePaymentUrl(onceOffPaymentModel);
        }
    }
}
