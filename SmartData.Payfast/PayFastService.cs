using SmartData.Payfast.Mappers;
using SmartData.Payfast.Models;

namespace SmartData.Payfast
{
    public class PayFastService: IPayFastService
    {
        private PayFastRequestMapper payFastRequestMapper;

        public PayFastService(PayFastRequestMapper payFastRequestMapper)
        {
            this.payFastRequestMapper = payFastRequestMapper;
        }

        public string CreateOncePaymentUrl(OnceOffPaymentModel onceOffPaymentModel)
        {
            PayFastRequest payFastRequest = payFastRequestMapper.MappToPayFastRequest(onceOffPaymentModel);

            return $"{onceOffPaymentModel.Merchant.ProcessUrl}{payFastRequest.ToString()}";
        }
    }
}
