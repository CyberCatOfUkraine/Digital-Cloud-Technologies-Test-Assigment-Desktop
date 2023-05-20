using DCTTA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for CurrenciesList.xaml
    /// </summary>
    public partial class CurrenciesList : UserControl
    {
        private ViewModels.SearchViewModel _viewModel = new();

        public List<Currency> Currencies { get; }

        public CurrenciesList(List<Currency> currencies)
        {
            InitializeComponent();
            Currencies = currencies;

            InitializeDataGrid();
            _viewModel.PropertyChanged += TextChanged;
            DataContext = _viewModel;
        }

        private void TextChanged(object? sender, PropertyChangedEventArgs e)
        {
            var searchViewModel = sender as ViewModels.SearchViewModel;
            foreach (var item in Currencies)
            {
                if (item.Code.ToLower().Contains(searchViewModel.Text.ToLower()) || item.Name.ToLower().Contains(searchViewModel.Text.ToLower()))
                {
                    item.IsCurrencyVisible = true;
                }
                else
                {
                    item.IsCurrencyVisible = false;
                }
            }
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            CryptoCurrenciesDataGrid.ItemsSource = null;
            CryptoCurrenciesDataGrid.ItemsSource = Currencies;
        }
        public Action<Currency> OnCurrencyDetailsShow { get; set; }
        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            if (OnCurrencyDetailsShow == null)
            {
                MessageBox.Show("Хтось проспав ініцалізацію події що мала б показувати деталі по крипті, і хто ж це може бути ?");
            }
            OnCurrencyDetailsShow.Invoke((Currency)CryptoCurrenciesDataGrid.CurrentItem);
        }
        public Action<Currency, List<Currency>> OnConvert;
        private void Convert_Click(object sender, RoutedEventArgs e)
        {

            if (OnConvert == null)
            {
                MessageBox.Show("Хтось проспав ініцалізацію події що мала б конвертувати одну крипту в іншу, і хто ж це може бути ?");
            }
            OnConvert.Invoke((Currency)CryptoCurrenciesDataGrid.CurrentItem, Currencies);
        }
    }

}
