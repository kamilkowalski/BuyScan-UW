using System;
using Windows.UI.Xaml.Data;
using Windows.Globalization.NumberFormatting;
using Windows.System.UserProfile;
using Windows.Storage;

namespace BuyScan_UW.Converters
{
    class DoubleToPriceConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var currency = GlobalizationPreferences.Currencies[0];

            if (localSettings.Values.ContainsKey("currency"))
            {
                currency = (string)localSettings.Values["currency"];
            }

            var currencyFormat = new CurrencyFormatter(currency);
            currencyFormat.FractionDigits = 2;

            //return currencyFormat.Format((double)value);
            return ((double)value).ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
