using ApiScraper.Scrapper;
using DCTTA.Fragments;
using DCTTA.Mappers;
using DCTTA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DCTTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CurrenciesList mainFragment;
        public MainWindow()
        {
            InitializeComponent();


            Task<List<Currency>> task = new(() =>
            {
                CoinCapScraper scraper = new();
                scraper.Initialize();
                return scraper.GetAll.Convert();
            });
            task.Start();
            mainFragment = new CurrenciesList(task.Result);
            mainFragment.OnCurrencyDetailsShow += ShowDetails;
            mainFragment.OnConvert += ConvertCurrencies;
            Container.Content = mainFragment;
        }

        private void ConvertCurrencies(Currency currency, List<Currency> currencies)
        {
            var convert = new CurrencyConverting(currency, currencies);
            convert.OnMainPageReturn += ReturnToMainPage;
            Container.Content = convert;
        }

        private void ShowDetails(Currency currency)
        {
            var details = new CurrencyDetails(currency);
            details.OnMainPageReturn += ReturnToMainPage;
            Container.Content = details;
        }

        private void ReturnToMainPage()
        {
            Container.Content = mainFragment;
        }
    }
}
