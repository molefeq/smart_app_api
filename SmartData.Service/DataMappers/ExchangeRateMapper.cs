using SmartData.DataAccess.Models;

using System;

using RM = SmartData.ReferenceApi.Models;

namespace SmartData.Service.DataMappers
{
    public class ExchangeRateMapper
    {
        public ExchangeRate MapToExchangeRate(RM.Rate rate, Country country, long baseCurrencyId)
        {
            return new ExchangeRate()
            {
                CountryId = country.Id,
                BaseCurrencyId = baseCurrencyId,
                Rate = rate.ExchangeRate,
                CreateDate = DateTime.Now
            };
        }

        public void MapToExchangeRate(ExchangeRate exchangeRate, RM.Rate rate)
        {
            exchangeRate.Rate = rate.ExchangeRate;
            exchangeRate.CreateDate = DateTime.Now;
        }
    }
}
