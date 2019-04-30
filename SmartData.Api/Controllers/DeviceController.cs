using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartData.Api.ActionResultHelpers;
using SmartData.Api.Extensions;
using SmartData.Data.ViewModels;
using SmartData.Data.ViewModels.Device;
using SmartData.Service.Device;
using System.Threading.Tasks;

namespace SmartData.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DeviceController : BaseController
    {
        private IDeviceService deviceService;
        private IConfiguration configuration;

        public DeviceController(IDeviceService deviceService, IConfiguration configuration)
        {
            this.deviceService = deviceService;
            this.configuration = configuration;
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
            
            //return Ok();

            return Ok(await deviceService.TopUpDevice(buyDataModel));
        }
    }
}
