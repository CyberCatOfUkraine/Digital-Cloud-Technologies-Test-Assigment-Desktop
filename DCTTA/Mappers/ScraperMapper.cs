using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTA.Mappers
{
    internal static class ScraperMapper
    {
        public static List<DCTTA.Models.Currency> Convert(this List<ApiScraper.Models.CryptocurrencyDetails> currencies)
        {
            if (currencies == null)
            {
                return new();
            }
            return (from currency in currencies
                    select new DCTTA.Models.Currency(
                currency.Code,
                currency.Name,
                currency.Price,
                currency.Volume,
                currency.PriceChange,
                currency.Markets.Convert(),
                currency.BaseId,
                currency.Rank, true)
                    ).ToList();
        }
        public static List<DCTTA.Models.Market> Convert(this List<ApiScraper.Models.Market> markets)
        {
            if (markets == null)
            {
                return new();
            }
            return (from market in markets select new DCTTA.Models.Market(market.Name, market.Price)).ToList();
        }
        public static List<ApiScraper.Models.Market> Convert(this List<DCTTA.Models.Market> markets)
        {
            if (markets == null)
            {
                return new();
            }
            return (from market in markets select new ApiScraper.Models.Market(market.Name, market.Price)).ToList();
        }
        public static ApiScraper.Models.CryptocurrencyDetails Convert(this DCTTA.Models.Currency currency)
        {
            return new(
                currency.Code,
                currency.Name,
                currency.Price,
                currency.Volume,
                currency.PriceChange,
                currency.Markets.Convert(),
                currency.BaseId,
                currency.Rank);
        }
    }
}
