using System;
using Windows.UI.Xaml.Data;

namespace BuyScan_UW.Converters
{
    class DoubleToPriceConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((double)value).ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
