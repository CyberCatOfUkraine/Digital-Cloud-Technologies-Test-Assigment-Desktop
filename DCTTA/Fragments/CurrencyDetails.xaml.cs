using DCTTA.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DCTTA.Fragments
{
    /// <summary>
    /// Interaction logic for Currency.xaml
    /// </summary>
    public partial class CurrencyDetails : UserControl
    {
        private Currency Currency { get; }
        public CurrencyDetails(Currency currency)
        {
            InitializeComponent();
            Currency = currency;

            CodeLabel.Content = currency.Code;
            NameLabel.Content = currency.Name;
            PriceLabel.Content = currency.Price;
            VolumeLabel.Content = currency.Volume;
            PriceChangeLabel.Content = currency.PriceChange;

            foreach (var market in currency.Markets)
            {
                var btn = new MarketButton(market.Name, currency.BaseId);
                btn.Content = $"{market.Name} ціна:{market.Price}";
                btn.Click += btn.MarketClick;
                btn.Margin = new Thickness(10, 0, 10, 0);
                MarketsPanel.Children.Add(btn);
            }
        }
        public Action OnMainPageReturn;

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            OnMainPageReturn.Invoke();
        }
    }

    internal class MarketButton : Button
    {
        public string Name { get; }
        public string CryptoCurrency { get; }

        public MarketButton(string name, string cryptoCurrency)
        {
            Name = name;
            CryptoCurrency = cryptoCurrency;
        }

        internal void MarketClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {

                var url = new ApiScraper.Scrapper.CoinCapScraper().GetMarketPage(CryptoCurrency, Name);

                System.Diagnostics.Process.Start("explorer.exe", $"\"{url}\"");
            });

        }

    }
}
