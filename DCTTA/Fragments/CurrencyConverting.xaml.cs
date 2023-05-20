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

namespace DCTTA.Fragments
{
    /// <summary>
    /// Interaction logic for CurrencyConverting.xaml
    /// </summary>
    public partial class CurrencyConverting : UserControl
    {
        public CurrencyConverting(Currency currency, List<Currency> currencies)
        {
            InitializeComponent();

            Currency = currency;
            Currencies = currencies;

            CurrencyNameLabel.Content = Currency.Name;

            InitializeStackPanel();
        }

        private void InitializeStackPanel()
        {
            foreach (var currency in Currencies)
            {
                var radioBtn = new CustomRadioButton(currency);

                radioBtn.Checked += CryptocurrencySelected;
                radioBtn.Content = currency.Name;
                CryptocurrenciesStackPanel.Children.Add(radioBtn);
            }
        }

        private void CryptocurrencySelected(object sender, RoutedEventArgs e)
        {
            var text = "Результат: ";
            var customRBtn = sender as CustomRadioButton;
            ConvertResultLabel.Content =text+ new ApiScraper.Scrapper.CoinCapScraper().Exchange(Convert.ToInt32(ValueTextBox.Text), Currency.Convert(), customRBtn.Currency.Convert());
            LastSelectedButton = customRBtn;
        }

        public Action OnMainPageReturn { get; internal set; }
        public Currency Currency { get; }
        public List<Currency> Currencies { get; }

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            OnMainPageReturn.Invoke();
        }

        class CustomRadioButton : RadioButton
        {
            public CustomRadioButton(Currency currency)
            {
                Currency = currency;
            }

            public Currency Currency { get; }
        }

        private void ValueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= int.MinValue + 1 && i <= int.MaxValue - 1;
        }
        CustomRadioButton LastSelectedButton;
        private void ValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsValid((sender as TextBox).Text) && LastSelectedButton != null)
            {
                ConvertResultLabel.Content = new ApiScraper.Scrapper.CoinCapScraper().Exchange(Convert.ToInt32(ValueTextBox.Text), Currency.Convert(), LastSelectedButton.Currency.Convert());
            }
        }
    }
}
