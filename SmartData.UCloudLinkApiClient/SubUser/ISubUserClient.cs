using SmartData.UCloudLinkApiClient.SubUser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.UCloudLinkApiClient.SubUser
{
    public interface ISubUserClient
    {
        Task CreateSubUser(CreateSubUserRequest createSubUserRequest, string accessToken);
        Task BindSubUserToDevice(BindSubUserToDeviceRequest bindSubUserToDeviceRequest, string accessToken);
        Task<List<SubUserResponse>> FetchSubUsersForBusinessUser(FetchSubUsersForBusinessUserRequest request, string accessToken);
        Task<TopUpForSubUserResponse> TopUpSubUser(TopUpForSubUserRequest request, string accessToken);
    }
}
