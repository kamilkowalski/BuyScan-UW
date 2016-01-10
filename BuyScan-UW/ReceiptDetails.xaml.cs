using BuyScan_UW.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
