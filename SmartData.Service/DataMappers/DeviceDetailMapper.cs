using SmartData.Data.ViewModels;
using SmartData.Data.ViewModels.Device;
using SmartData.DataAccess.Models;

using SmartData.UCloudLinkApiClient.SubUser.Models;

using SqsLibraries.Common.Utilities;

using System;

namespace SmartData.Service.DataMappers
{
    public class DeviceDetailMapper
    {
        public DeviceDetailModel MapToDeviceDetailModel(DeviceDetail deviceDetail)
        {
            return new DeviceDetailModel()
            {
                Id = deviceDetail.Id,
                DeviceId = deviceDetail.DeviceId,
                DeviceName = deviceDetail.DeviceName,
                SerailNumber = deviceDetail.SerailNumber,
                LinkedUserId = deviceDetail.LinkedUserId,
                // LinkedUserName = deviceDetail.LinkedUser == null ? "" :  `${ deviceDetail.LinkedUser.Firstname} ${ deviceDetail.LinkedUser.LastName}`,
                StatusId = deviceDetail.StatusId,
                StatusName = deviceDetail.Status.Name
            };
        }

        public DeviceDetail MapToDeviceDetail(LinkDeviceModel linkDeviceModel)
        {
            return new DeviceDetail()
            {
                DeviceName = linkDeviceModel.DeviceName,
                SerailNumber = linkDeviceModel.SerialNumber,
                LinkedUserId = linkDeviceModel.UserId,
                CreateUserId = linkDeviceModel.UserId,
                CreateDate = DateTime.Now
            };
        }

        public void MapToDeviceDetail(DeviceDetail deviceDetail, LinkDeviceModel linkDeviceModel)
        {
            deviceDetail.DeviceName = linkDeviceModel.DeviceName;
            deviceDetail.LinkedUserId = linkDeviceModel.UserId;
            deviceDetail.ModifiedUserId = linkDeviceModel.UserId;
            deviceDetail.ModifiedDate = DateTime.Now;
        }

        public DeviceDetailModel MapToDeviceDetailModel(SubUserResponse subUserResponse)
        {
            return new DeviceDetailModel()
            {
                SerailNumber = subUserResponse.Imei,
                Balance = subUserResponse.Balance,
                CurrencyType = subUserResponse.CurrencyType,
                LastDeviceCheck = DateTime.Now.ToString(Constants.DateTimeFormat)
            };
        }

        public void MapToDeviceDetailModel(DeviceDetailModel deviceDetailModel, DeviceDetail deviceDetail)
        {
            deviceDetailModel.Id = deviceDetail.Id;
            deviceDetailModel.DeviceId = deviceDetail.DeviceId;
            deviceDetailModel.DeviceName = deviceDetail.DeviceName;
            deviceDetailModel.LinkedUserId = deviceDetail.LinkedUserId;
        }
    }
}
