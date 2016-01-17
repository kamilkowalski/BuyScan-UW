using BuyScanModels.Models;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BuyScan_UW
{
    public sealed partial class Receipts : UserControl
    {
        private MainPage mainPage;

        public Receipts(MainPage mainPage)
        {
            this.mainPage = mainPage;
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

        private void ReceiptClicked(object sender, ItemClickEventArgs e)
        {
            var receipt = (Receipt) e.ClickedItem;
            if (receipt.IsProcessed)
            {
                mainPage.Frame.Navigate(typeof(ReceiptDetails), receipt);
            }
        }
    }
}
