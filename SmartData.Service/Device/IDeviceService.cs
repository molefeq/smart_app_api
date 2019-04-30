using SmartData.Data.ViewModels;
using SmartData.Data.ViewModels.Device;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.Service.Device
{
    public interface IDeviceService
    {
        Task<DeviceDetailModel> GetDevice(string username);
        List<DeviceDetailModel> GetUserLinkedDevices(long userId);
        void UnlinkUserFromDevice(UnLinkDeviceModel unLinkDeviceModel);
        Task<DeviceDetailModel> LinkUserToDevice(LinkDeviceModel linkDeviceModel);
        Task<DeviceDetailModel> TopUpDevice(BuyDataModel buyDataModel);
    }
}
