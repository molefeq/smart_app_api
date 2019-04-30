using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartData.Api.Extensions;
using SmartData.Api.Extensions.ModelBinders;
using SmartData.Data.ViewModels;
using SmartData.Payfast.Models;
using SmartData.Service.Payment;
using System.Threading.Tasks;

namespace SmartData.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class PayFastController : BaseController
    {
        private readonly PayFastSettings payFastSettings;
        private readonly ILogger logger;
        private IPaymentService paymentService;

        public PayFastController(IOptions<PayFastSettings> payFastSettings, ILogger<PayFastController> logger, IPaymentService paymentService)
        {
            this.payFastSettings = payFastSettings.Value;
            this.logger = logger;
            this.paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult OnceOffPayment(BuyDataModel buyDataModel)
        {
            SetBuyerDetails(buyDataModel);

            var url = paymentService.GetOnceOffPaymentUrl(buyDataModel, payFastSettings);

            return Ok(new { url = url });
        }

        [HttpPost]
        public async Task<IActionResult> Notify([ModelBinder(BinderType = typeof(PayFastNotifyModelBinder))]PayFastNotify payFastNotifyViewModel)
        {
            payFastNotifyViewModel.SetPassPhrase(this.payFastSettings.PassPhrase);

            var calculatedSignature = payFastNotifyViewModel.GetCalculatedSignature();

            var isValid = payFastNotifyViewModel.signature == calculatedSignature;

            this.logger.LogInformation($"Signature Validation Result: {isValid}");

            // The PayFast Validator is still under developement
            // Its not recommended to rely on this for production use cases
            var payfastValidator = new PayFastValidator(this.payFastSettings, payFastNotifyViewModel, this.HttpContext.Connection.RemoteIpAddress);

            var merchantIdValidationResult = payfastValidator.ValidateMerchantId();

            this.logger.LogInformation($"Merchant Id Validation Result: {merchantIdValidationResult}");

            var ipAddressValidationResult = await payfastValidator.ValidateSourceIp();

            this.logger.LogInformation($"Ip Address Validation Result: {merchantIdValidationResult}");

            // Currently seems that the data validation only works for success
            if (payFastNotifyViewModel.payment_status == PayFastStatics.CompletePaymentConfirmation)
            {
                var dataValidationResult = await payfastValidator.ValidateData();

                this.logger.LogInformation($"Data Validation Result: {dataValidationResult}");
            }

            if (payFastNotifyViewModel.payment_status == PayFastStatics.CancelledPaymentConfirmation)
            {
                this.logger.LogInformation($"Subscription was cancelled");
            }

            return Ok();
        }
    }
}