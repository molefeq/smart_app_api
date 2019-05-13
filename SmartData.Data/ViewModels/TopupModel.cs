using System;
using System.Collections.Generic;
using System.Text;

namespace SmartData.Data.ViewModels
{
    public class TopupModel
    {
        public int DataQuantity { get; set; }
        public string DataScale { get; set; }
        public string DataQuantityDescription { get; set; }
        public decimal Amount { get; set; }
        public decimal ExchangeRateAmount { get; set; }
        public string ExchangeRateSymbol { get; set; }
        public long ExchangeRateCurrencyId { get; set; }
        public string Currency { get; set; }
    }
}
