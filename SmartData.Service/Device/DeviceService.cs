using Microsoft.Extensions.Caching.Memory;
using SmartData.Common.Extensions;
using SmartData.Data.ViewModels;
using SmartData.Data.ViewModels.Device;
using SmartData.DataAccess;
using SmartData.DataAccess.Models;
using SmartData.Service.DataMappers;
using SmartData.Service.RequestMappers;
using SmartData.UCloudLinkApiClient.SubUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartData.Service.Device
{
    public class DeviceService : IDeviceService
    {
        private IUnitOfWork unitOfWork;
        private DeviceDetailMapper deviceDetailMapper;
        private ISubUserClient subUserClient;
        private IMemoryCache cache;
        private FetchSubUsersForBusinessUserRequestMapper fetchSubUsersForBusinessUserRequestMapper;
        private TopUpForSubUserRequestMapper topUpForSubUserRequestMapper;
        private BindSubUserToDeviceRequestMapper bindSubUserToDeviceRequest;

        public DeviceService(IUnitOfWork unitOfWork,
            IMemoryCache cache,
            DeviceDetailMapper deviceDetailMapper,
            ISubUserClient subUserClient,
            FetchSubUsersForBusinessUserRequestMapper fetchSubUsersForBusinessUserRequestMapper,
            TopUpForSubUserRequestMapper topUpForSubUserRequestMapper,
            BindSubUserToDeviceRequestMapper bindSubUserToDeviceRequest)
        {
            this.unitOfWork = unitOfWork;
            this.cache = cache;
            this.deviceDetailMapper = deviceDetailMapper;
            this.subUserClient = subUserClient;
            this.fetchSubUsersForBusinessUserRequestMapper = fetchSubUsersForBusinessUserRequestMapper;
            this.topUpForSubUserRequestMapper = topUpForSubUserRequestMapper;
            this.bindSubUserToDeviceRequest = bindSubUserToDeviceRequest;
        }

        public async Task<DeviceDetailModel> GetDevice(string username)
        {
            var subUsers = await subUserClient.FetchSubUsersForBusinessUser(fetchSubUsersForBusinessUserRequestMapper.MapToRequest(username), cache.AccessToken());

            if (subUsers == null || subUsers.Count == 0 || subUsers.Where(item => item.UserCode.Equals(username)).FirstOrDefault() == null)
            {
                return null;
            }

            var subUser = subUsers.Where(item => item.UserCode.Equals(username)).FirstOrDefault();
            var device = unitOfWork.DeviceDetail.GetById(d => d.SerailNumber == subUser.Imei);
            var deviceDetailModel = deviceDetailMapper.MapToDeviceDetailModel(subUser);

            deviceDetailMapper.MapToDeviceDetailModel(deviceDetailModel, device);

            return deviceDetailModel;
        }

        public List<DeviceDetailModel> GetUserLinkedDevices(long userId)
        {
            var user = unitOfWork.Account.GetById(a => a.Id == userId, "LinkedDevices.LinkedUser,LinkedDevices.Status");

            return user.LinkedDevices.Select(item => deviceDetailMapper.MapToDeviceDetailModel(item)).ToList();
        }

        public async Task<DeviceDetailModel> LinkUserToDevice(LinkDeviceModel linkDeviceModel)
        {
            await subUserClient.BindSubUserToDevice(bindSubUserToDeviceRequest.MapToRequest(linkDeviceModel), cache.AccessToken());
            var device = unitOfWork.DeviceDetail.GetById(d => d.SerailNumber == linkDeviceModel.SerialNumber);

            if (device == null)
            {
                return await AddDevice(linkDeviceModel);
            }

            return await UpdateDevice(device, linkDeviceModel);
        }

        public void UnlinkUserFromDevice(UnLinkDeviceModel unLinkDeviceModel)
        {
            var device = unitOfWork.DeviceDetail.GetById(d => d.Id == unLinkDeviceModel.Id);

            device.LinkedUserId = null;
            device.ModifiedDate = DateTime.Now;
            device.ModifiedUserId = unLinkDeviceModel.UserId;

            unitOfWork.DeviceDetail.Update(device);
            unitOfWork.Save();
        }

        public async Task<DeviceDetailModel> TopUpDevice(BuyDataModel buyDataModel)
        {
            var topUpForSubUserResponse = await subUserClient.TopUpSubUser(topUpForSubUserRequestMapper.MapToRequest(buyDataModel), cache.AccessToken());

            return await GetDevice(buyDataModel.EmailAddress);
        }
        
        public async Task<DeviceDetailModel> AddDevice(LinkDeviceModel linkDeviceModel)
        {
            var device = deviceDetailMapper.MapToDeviceDetail(linkDeviceModel);
            unitOfWork.DeviceDetail.Insert(device);

            unitOfWork.Save();

            return await GetDevice(linkDeviceModel.EmailAddress);
        }

        private async Task<DeviceDetailModel> UpdateDevice(DeviceDetail deviceDetail, LinkDeviceModel linkDeviceModel)
        {
            deviceDetailMapper.MapToDeviceDetail(deviceDetail, linkDeviceModel);
            unitOfWork.DeviceDetail.Update(deviceDetail);

            unitOfWork.Save();

            return await GetDevice(linkDeviceModel.EmailAddress);
        }
    }
}
