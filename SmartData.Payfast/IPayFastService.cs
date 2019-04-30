using SmartData.Payfast.Models;

namespace SmartData.Payfast
{
    public interface IPayFastService
    {
        string CreateOncePaymentUrl(OnceOffPaymentModel onceOffPaymentModel);
    }
}
