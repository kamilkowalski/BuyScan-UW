using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.System.UserProfile;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BuyScan_UW
{
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            InitCurrencies();
        }

        private void InitCurrencies()
        {
            var currencyList = new List<Currency>();
            currencyList.Add(new Currency("PLN", "polski złoty"));
            currencyList.Add(new Currency("USD", "american dollar"));
            currencyList.Add(new Currency("EUR", "euro"));
            currencyList.Add(new Currency("GBP", "british pound"));

            Currency selectedCurrency = currencyList.First(c => c.Code == GetSelectedCurrencyCode());
            CurrencySelect.ItemsSource = currencyList;
            CurrencySelect.SelectedItem = selectedCurrency;
        }

        private string GetSelectedCurrencyCode()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var currency = GlobalizationPreferences.Currencies[0];

            if (localSettings.Values.ContainsKey("currency"))
            {
                currency = (string)localSettings.Values["currency"];
            }

            return currency;
        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                this.Frame.Navigate(typeof(MainPage), "Settings");
            }
        }

        public class Currency
        {
            public string Code { get; set; }
            public string Name { get; set; }

            public Currency(string code, string name)
            {
                Code = code;
                Name = name;
            }
        }

        private void UpdateAutocorrectSetting(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["autocorrect"] = AutocorrectToggle.IsOn;
        }

        private void UpdateCurrencySetting(object sender, SelectionChangedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var currency = (Currency)CurrencySelect.SelectedItem;
            localSettings.Values["currency"] = currency.Code;
        }
    }
}
