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
using Windows.Media.Capture;
using Windows.Storage;
using BuyScan_UW.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BuyScan_UW
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private static Receipts ReceiptsView;
        private static Expenses ExpensesView;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnPivotItemLoading(Pivot sender, PivotItemEventArgs e)
        {
            if (e.Item.Content != null)
            {
                return;
            }
            
            var pivotItemContentControl = CreateUserControlForPivotItem(((Pivot)sender).SelectedIndex);
            e.Item.Content = pivotItemContentControl;
        }

        private static UserControl CreateUserControlForPivotItem(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    if (ExpensesView == null) ExpensesView = new Expenses();
                    return ExpensesView;
                case 1:
                    if (ReceiptsView == null) ReceiptsView = new Receipts();
                    return ReceiptsView;
                default:
                    throw new ArgumentOutOfRangeException("selectedIndex");
            }
        }

        private async void OpenCamera(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }

            using (var db = new ReceiptContext())
            {
                var receipt = new Receipt { ImagePath = photo.Path, CreatedAt = DateTime.Now };
                db.Receipts.Add(receipt);
                db.SaveChanges();
            }

            PrimaryPivot.SelectedIndex = 1;
            if(ReceiptsView != null)
            {
                ReceiptsView.ReloadReceipts();
            }
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {

        }
    }
}
