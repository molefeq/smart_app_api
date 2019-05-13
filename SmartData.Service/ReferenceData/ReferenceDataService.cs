using SmartData.Data.ViewModels;
using SmartData.DataAccess;
using SmartData.ReferenceApi.OpenRatesApi;
using SmartData.ReferenceApi.RestCountriesApi;
using SmartData.Service.DataMappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartData.Service.ReferenceData
{
    public class ReferenceDataService : IReferenceDataService
    {
        private IUnitOfWork unitOfWork;
        private ICountryRestService countryRestService;
        private IExchangeRateService exchangeRateService;
        private CountryMapper countryMapper;
        private ExchangeRateMapper exchangeRateMapper;

        public ReferenceDataService(IUnitOfWork unitOfWork,
                                    ICountryRestService countryRestService,
                                    IExchangeRateService exchangeRateService,
                                    CountryMapper countryMapper,
                                    ExchangeRateMapper exchangeRateMapper)
        {
            this.unitOfWork = unitOfWork;
            this.countryRestService = countryRestService;
            this.exchangeRateService = exchangeRateService;
            this.countryMapper = countryMapper;
            this.exchangeRateMapper = exchangeRateMapper;
        }

        public StaticDataModel GetStaticData()
        {
            return new StaticDataModel
            {
                Countries = GetCountries()
            };
        }

        public List<ReferenceDataModel> GetCountries()
        {
            var countries = unitOfWork.Country.GetEntities();

            return countries.Select(c => new ReferenceDataModel(c.Id, c.Name, c.Code)).ToList();
        }

        public async Task BulkInsertCountries()
        {
            var savedCountries = unitOfWork.Country.GetEntities().ToList();
            var countries = await countryRestService.GetCountries();

            if (countries == null || countries.Count() == 0)
            {
                return;
            }

            foreach (var country in countries)
            {
                var savedCountry = savedCountries.Where(item => item.Code.Equals(country.Alpha3Code)).FirstOrDefault();
                var currency = country.Currencies.FirstOrDefault();
                var dbCurrency = unitOfWork.Currency.GetById(item => currency != null && item.Code.Equals(currency.Code));

                if (savedCountry == null)
                {
                    unitOfWork.Country.Insert(this.countryMapper.MapToCountry(country, dbCurrency));
                    continue;
                }

                this.countryMapper.MapToCountry(savedCountry, country, dbCurrency);
                unitOfWork.Country.Update(savedCountry);
            }


            unitOfWork.Save();
        }

        public async Task BulkInsertExchangeRates(string baseCurrency)
        {
            var dbExchangeRates = unitOfWork.ExchangeRate.GetEntities().ToList();
            var rates = await exchangeRateService.GetRates(baseCurrency);

            if (rates == null || rates.Count() == 0)
            {
                return;
            }

            foreach (var rate in rates)
            {
                var currency = unitOfWork.Currency.GetById(item => item.Code.Equals(rate.Currency));
                var baseCurrencyItem = unitOfWork.Currency.GetById(item => item.Code.Equals(baseCurrency));
                var countries = unitOfWork.Country.GetEntities(item => item.CurrencyId == currency.Id).ToList();

                foreach (var country in countries)
                {
                    var dbExchangeRate = dbExchangeRates.Where(item => baseCurrency.Equals(item.BaseCurrency) && item.CountryId == country.Id).FirstOrDefault();

                    if (dbExchangeRate == null)
                    {
                        unitOfWork.ExchangeRate.Insert(this.exchangeRateMapper.MapToExchangeRate(rate, country, baseCurrencyItem.Id));
                        continue;
                    }

                    this.exchangeRateMapper.MapToExchangeRate(dbExchangeRate, rate);
                    unitOfWork.ExchangeRate.Update(dbExchangeRate);
                }
            }

            unitOfWork.Save();
        }
    }
}
