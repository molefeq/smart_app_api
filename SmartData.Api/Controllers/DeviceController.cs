using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartData.Api.ActionResultHelpers;
using SmartData.Api.Extensions;
using SmartData.Data.ViewModels;
using SmartData.Data.ViewModels.Device;
using SmartData.Service.Device;
using SmartData.Service.Payment;
using SmartData.Service.Topup;
using SmartData.UCloudLinkApiClient.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class DeviceController : BaseController
    {
        private IDeviceService deviceService;
        private IPaymentService paymentService;
        private ITopupService topupService;

        public DeviceController(IDeviceService deviceService, IPaymentService paymentService, ITopupService topupService)
        {
            this.deviceService = deviceService;
            this.paymentService = paymentService;
            this.topupService = topupService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeviceDetailModel), 200)]
        public async Task<IActionResult> LinkDevice([FromBody] LinkDeviceModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ValidationActionResult(ModelState);
            }

            model.UserId = UserId.Value;
            model.EmailAddress = EmailAddress;

            return Ok(await deviceService.LinkUserToDevice(model));
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeviceDetailModel), 200)]
        public IActionResult UnLinkDevice([FromBody] UnLinkDeviceModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ValidationActionResult(ModelState);
            }

            model.UserId = UserId.Value;
            deviceService.UnlinkUserFromDevice(model);

            return Ok();
        }

        [HttpGet()]
        [ProducesResponseType(typeof(DeviceDetailModel), 200)]
        public async Task<IActionResult> GetDeviceInformation()
        {
            return Ok(await deviceService.GetDevice(EmailAddress));
        }

        [HttpPost()]
        [ProducesResponseType(typeof(DeviceDetailModel), 200)]
        public async Task<IActionResult> TopUpDevice(BuyDataModel buyDataModel)
        {
            SetBuyerDetails(buyDataModel);

            try
            {
                var deviceInformation = await deviceService.TopUpDevice(buyDataModel);
                return Ok(deviceInformation);
            }
            catch (UCloudlinkInvalidResponseException ex)
            {
                paymentService.FailOnceOffPayment(buyDataModel.PaymentId);
                throw;
            }

            //return Ok();
        }

        [HttpPost()]
        [ProducesResponseType(typeof(List<TopupModel>), 200)]
        public async Task<IActionResult> FindTopOptions(GeoLocation geoLocation)
        {
            var topuptions = await topupService.GetTopupOptions(geoLocation, 5);// UserId.Value);
            return Ok(topuptions);
        }
    }
}
