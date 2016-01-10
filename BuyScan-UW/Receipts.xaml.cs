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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BuyScan_UW
{
    public sealed partial class Receipts : UserControl
    {
        public Receipts()
        {
            this.InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            ReloadReceipts();
        }

        public void ReloadReceipts()
        {
            using (var db = new ReceiptContext())
            {
                var receipts = db.Receipts.ToList();
                var items = db.ReceiptItems.ToList();

                ReceiptsList.ItemsSource = receipts;
            }
        }
    }
}
