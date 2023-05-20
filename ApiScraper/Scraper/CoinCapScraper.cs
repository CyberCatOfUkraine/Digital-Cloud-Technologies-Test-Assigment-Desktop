using AngleSharp;
using ApiScraper.Models;
using ApiScraper.Models.ForParser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiScraper.Scrapper
{
    public class CoinCapScraper : IScraper<CryptocurrencyDetails>
    {
        private static List<CryptocurrencyDetails> Cryptocurrencies;
        public CoinCapScraper()
        {
            Cryptocurrencies = new();
        }
        public void Initialize()
        {
            UpdateCryptocurrenciesList();
        }
        public List<CryptocurrencyDetails> GetAll => Cryptocurrencies;

        public decimal Exchange(int value, CryptocurrencyDetails first, CryptocurrencyDetails second)
        {
            decimal result = value * first.Price / second.Price;
            return result;
        }

        public CryptocurrencyDetails GetByCode(string code)
        {
            if (Cryptocurrencies.Exists(x => x.Code == code))
            {
                return Cryptocurrencies.Find(x => x.Code == code);
            }
            else
                return null;
        }

        public CryptocurrencyDetails Get(string Name)
        {
            if (Cryptocurrencies.Exists(x => x.Name == Name))
            {
                return Cryptocurrencies.Find(x => x.Name == Name);
            }
            else
                return null;
        }

        public void UpdateCryptocurrenciesList()
        {
            Cryptocurrencies = GetCryptocurrencyList();

        }

        private List<CryptocurrencyDetails> GetCryptocurrencyList()
        {
            return TrasformToCryptocurrencyDetails(GetStringByUrl(Resources.Assets));
        }

        private List<CryptocurrencyDetails> TrasformToCryptocurrencyDetails(string text)
        {
            List<CryptocurrencyDetails> details = new();
            var root = JsonConvert.DeserializeObject<CoinCapRoot>(text);
            foreach (var coinCapDetails in root.data)
            {
                details.Add(new CryptocurrencyDetails(
                    coinCapDetails.symbol,
                    coinCapDetails.name,
                    Convert.ToDecimal(coinCapDetails.priceUsd.Replace(".", ",")),
                    Convert.ToDecimal(coinCapDetails.supply.Replace(".", ",")),
                    Convert.ToDecimal(coinCapDetails.changePercent24Hr.Replace(".", ",")),
                    GetMarketsByCryptoCurrencyName(coinCapDetails.id),
                    coinCapDetails.id, Convert.ToInt32(coinCapDetails.rank)));
            }
            return details;
        }
        private List<Market> GetMarketsByCryptoCurrencyName(string cryptocurrencyId)
        {
            var result = new List<Market>();

            var marketsString = GetStringByUrl(Resources.MarketsByBaseID + cryptocurrencyId);

            var markets = JsonConvert.DeserializeObject<CoinCapMarketRoot>(marketsString).data;

            foreach (var market in markets.Where(x => x.priceUsd != "null" && x.quoteSymbol.ToLower() == "usd"))
            {
                result.Add(new Market(market.exchangeId, Convert.ToDecimal(market.priceUsd.Replace(".", ","))));
            }
            return result;
        }
        public string GetMarketPage(string cryptocurrencyId, string exchangeId)
        {
            var url = "https://www.google.com/search?q=" + exchangeId + "+" + cryptocurrencyId + "+buy";

            try
            {
                var html = GetStringByUrl(url);
                /*
                //HtmlAgilityPack version 

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                var node = htmlDocument.DocumentNode;
                var html = node.ChildNodes[1]
                    .ChildNodes[1].
                    ChildNodes[1];
                var table1 = html.
                    ChildNodes[4].
                    ChildNodes[0].
                    ChildNodes[0];
                var table = table1.
                ChildNodes[0].
                Attributes[0].
                Value;
                table = table.Substring(table.IndexOf("http"), table.IndexOf(";"));
                if (table.Contains(";sa=U&a"))
                {
                    table = table.Replace("&amp;sa=U&a", string.Empty);
                }
                return table;*/


                var config = Configuration.Default;
                using var context = BrowsingContext.New(config);
                using var doc = context.OpenAsync(req => req.Content(html)).Result;
                var linkNode = doc.Children[0].Children[1].Children[1].
                    Children[5].Children[0].Children[0].Children[0];

                string oterHtml = linkNode.OuterHtml;

                string pattern = @"href=""/url\?q=(.*?)&amp;";
                Match match = Regex.Match(oterHtml, pattern);

                string link = url;
                if (match.Success)
                {
                    link = match.Groups[1].Value;
                    link = Uri.UnescapeDataString(link);

                }
                return link;
            }
            catch (Exception)
            {
                //TODO: Додати ведення логів в разі потреби
            }
            return url;
        }
        public bool IsCoinCapAviable()
        {
            return !string.IsNullOrEmpty(GetStringByUrl(Resources.Assets));
        }
        private string GetStringByUrl(string url)
        {
            return new WebScraper().GetTextByUrl(url);
        }

    }
}
