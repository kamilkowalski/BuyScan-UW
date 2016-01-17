using BuyScanModels.Models;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BuyScan_UW
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReceiptDetails : Page
    {
        public ReceiptDetails()
        {
            this.InitializeComponent();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            if(!e.Handled)
            {
                e.Handled = true;
                this.Frame.Navigate(typeof(MainPage), "ReceiptDetails");
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Receipt)
            {
                var receipt = (Receipt)e.Parameter;
                var bitmap = new BitmapImage();
                bitmap.UriSource = new Uri(receipt.ImagePath, UriKind.Absolute);
                ReceiptImage.Source = bitmap;

                ReceiptItemsList.ItemsSource = receipt.ReceiptItems;
            }
            base.OnNavigatedTo(e);
        }
    }
}
