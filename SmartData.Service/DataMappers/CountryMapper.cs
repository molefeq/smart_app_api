using SmartData.DataAccess.Models;
using System.Linq;
using RM = SmartData.ReferenceApi.Models;
namespace SmartData.Service.DataMappers
{
    public class CountryMapper
    {
        public Country MapToCountry(RM.Country country, Currency dbCurrency)
        {
            var currency = country.Currencies.FirstOrDefault();

            if (dbCurrency == null && currency != null && !string.IsNullOrEmpty(currency.Code))
            {
                dbCurrency = new Currency()
                {
                    Code = currency.Code
                };
            }

            return new Country()
            {
                Code = country.Alpha3Code,
                Name = country.Name,
                IsUcloudEnabled = false,
                Currency = dbCurrency
            };
        }
        public void MapToCountry(Country dbCountry, RM.Country country, Currency dbCurrency)
        {
            var currency = country.Currencies.FirstOrDefault();

            if (dbCurrency == null && currency != null && !string.IsNullOrEmpty(currency.Code))
            {
                dbCurrency = new Currency()
                {
                    Code = currency.Code
                };
            }

            dbCountry.Currency = dbCurrency;
            dbCountry.Name = country.Name;
            dbCountry.Code = country.Alpha3Code;
        }
    }
}
