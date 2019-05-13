using SmartData.Common.Exceptions;
using SmartData.Data.ViewModels;
using SmartData.DataAccess;
using SmartData.ReferenceApi.HereApi;
using SmartData.Service.DataMappers;
using SqsLibraries.Common.Utilities.ResponseObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartData.Service.Topup
{
    public class TopupService : ITopupService
    {
        private IUnitOfWork unitOfWork;
        private IReverseGeocodeService reverseGeocodeService;
        private TopupOptionMapper topupOptionMapper;

        public TopupService(IUnitOfWork unitOfWork, IReverseGeocodeService reverseGeocodeService, TopupOptionMapper topupOptionMapper)
        {
            this.unitOfWork = unitOfWork;
            this.reverseGeocodeService = reverseGeocodeService;
            this.topupOptionMapper = topupOptionMapper;
        }

        public async Task<List<TopupModel>> GetTopupOptions(GeoLocation geoLocation, long userId)
        {
            var geoCodeAddress = await reverseGeocodeService.ReverseGeocode(geoLocation.Latitude, geoLocation.Longitude);
            var currentCountry = unitOfWork.Country.GetById(item => item.Code.Equals(geoCodeAddress.Country), "Tier.TopupOption.Currency");
            var topupOptions = currentCountry?.Tier?.TopupOption;

            if (topupOptions.Count == 0 || topupOptions == null)
            {
                throw new ResponseValidationException(ResponseMessage.ToError("Your current location does not have any top option available."));
            }

            var userCountry = unitOfWork.Account.GetById(item => item.Id == userId, "Country.Currency").Country;
            var topupOptionModels = new List<TopupModel>();

            foreach (var topupOption in topupOptions)
            {
                var exchangeRate = unitOfWork.ExchangeRate.GetById(item => item.CountryId == userCountry.Id && item.BaseCurrencyId == topupOption.CurrencyId);

                topupOptionModels.Add(topupOptionMapper.MapToTopupModel(topupOption, exchangeRate, userCountry.Currency));
            }


            return topupOptionModels;
        }
    }
}
