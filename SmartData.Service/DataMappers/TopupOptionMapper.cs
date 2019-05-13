using SmartData.Data.ViewModels;
using SmartData.DataAccess.Models;

namespace SmartData.Service.DataMappers
{
    public class TopupOptionMapper
    {        
        public TopupModel MapToTopupModel(TopupOption topupOption, ExchangeRate exchangeRate, Currency  exchangeRateCurrency)
        {
            TopupModel topupModel = new TopupModel
            {
                DataQuantity = topupOption.DataQuantity,
                DataScale = topupOption.DataScale,
                DataQuantityDescription = topupOption.DataQuantityDescription,
                ExchangeRateAmount = exchangeRate == null ? topupOption.Amount : exchangeRate.Rate * topupOption.Amount,
                ExchangeRateSymbol = exchangeRateCurrency.Symbol,
                ExchangeRateCurrencyId = exchangeRateCurrency.Id,
                Amount = topupOption.Amount,
                Currency = topupOption.Currency.Code,
            };

            return topupModel;
        }

    }
}
