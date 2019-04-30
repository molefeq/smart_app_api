using Newtonsoft.Json.Linq;
using SmartData.UCloudLinkApiClient.SubUser.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartData.UCloudLinkApiClient.SubUser
{
    public class SubUserClient : ISubUserClient
    {
        private readonly UCloudLinkClient _uCloudLinkClient;

        public SubUserClient(UCloudLinkClient uCloudLinkClient)
        {
            _uCloudLinkClient = uCloudLinkClient;
        }

        public async Task CreateSubUser(CreateSubUserRequest createSubUserRequest, string accessToken)
        {
            var response = await _uCloudLinkClient.PostAsync(createSubUserRequest, $"user/BatchCreateGroupChild?access_token={accessToken}");
        }

        public async Task BindSubUserToDevice(BindSubUserToDeviceRequest bindSubUserToDeviceRequest, string accessToken)
        {
            var response = await _uCloudLinkClient.PostAsync(bindSubUserToDeviceRequest, $"tml/BindDevice?access_token={accessToken}");
        }

        public async Task<List<SubUserResponse>> FetchSubUsersForBusinessUser(FetchSubUsersForBusinessUserRequest request, string accessToken)
        {
            var response = await _uCloudLinkClient.PostAsync(request, $"user/QuerySubUserListInfo?access_token={accessToken}");
            var jsonResponse = JObject.Parse(response);
            var subUsers = from item in jsonResponse["data"]["dataList"]
                           select item.ToObject<SubUserResponse>();

            return subUsers.ToList();
        }

        public async Task<TopUpForSubUserResponse> TopUpSubUser(TopUpForSubUserRequest request, string accessToken)
        {
            var response = await _uCloudLinkClient.PostAsync(request, $"user/QuerySubUserListInfo?access_token={accessToken}");
            var jsonResponse = JObject.Parse(response);

            return jsonResponse["data"].ToObject<TopUpForSubUserResponse>();
        }
    }
}
