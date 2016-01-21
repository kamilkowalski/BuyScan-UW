using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using Windows.Storage;
using BuyScanModels.Models;
using Windows.Web.Http;
using Windows.Data.Json;
using Windows.ApplicationModel.Background;
using System.Linq;

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
            //Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                if (this.Frame.CanGoBack) {
                    this.Frame.GoBack();
                }
            }
        }

        private void OnPivotItemLoading(Pivot sender, PivotItemEventArgs e)
        {
            if (e.Item.Content != null)
            {
                return;
            }
            
            var pivotItemContentControl = CreateUserControlForPivotItem(sender.SelectedIndex);
            e.Item.Content = pivotItemContentControl;
        }

        private UserControl CreateUserControlForPivotItem(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    if (ExpensesView == null) ExpensesView = new Expenses();
                    return ExpensesView;
                case 1:
                    if (ReceiptsView == null) ReceiptsView = new Receipts(this);
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

            StorePhoto(photo);

            PrimaryPivot.SelectedIndex = 1;
            ReloadReceipts();
        }

        private async void StorePhoto(StorageFile photo)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri("http://api.kamilkowalski.pl/receipts");
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                httpResponse = await httpClient.PostAsync(requestUri, null);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            JsonValue responseValue = JsonValue.Parse(httpResponseBody);
            string referenceId = responseValue.GetObject().GetNamedString("id");

            using (var db = new ReceiptContext())
            {
                var receipt = new Receipt { Reference = referenceId, ImagePath = photo.Path, CreatedAt = DateTime.Now, IsProcessed = false };
                db.Receipts.Add(receipt);
                db.SaveChanges();
            }

            RegisterFetchReceiptItemsTask();
            FetchReceiptItemsNow();
        }

        private async void RegisterFetchReceiptItemsTask()
        {
            var taskRegistered = false;
            var exampleTaskName = "FetchReceiptItemsTask";

            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == exampleTaskName)
                {
                    taskRegistered = true;
                    break;
                }
            }

            if (!taskRegistered)
            {
                var builder = new BackgroundTaskBuilder();

                builder.Name = exampleTaskName;
                builder.TaskEntryPoint = "BuyScanBackgroundTasks.FetchReceiptItemsTask";
                builder.SetTrigger(new SystemTrigger(SystemTriggerType.InternetAvailable, false));
                
                var access = await BackgroundExecutionManager.RequestAccessAsync();

                BackgroundTaskRegistration task = builder.Register();
                task.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);
            }
        }

        private async void FetchReceiptItemsNow()
        {
            using (var db = new ReceiptContext())
            {
                var receipts = db.Receipts.Where(r => r.IsProcessed == false).ToList();

                foreach (Receipt receipt in receipts)
                {
                    HttpClient httpClient = new HttpClient();
                    Uri requestUri = new Uri("http://api.kamilkowalski.pl/receipts/" + receipt.Reference + "/?t=" + DateTime.Now.ToString());
                    HttpResponseMessage httpResponse = new HttpResponseMessage();
                    string httpResponseBody = "";

                    try
                    {
                        httpResponse = await httpClient.GetAsync(requestUri);
                        httpResponse.EnsureSuccessStatusCode();
                        httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                    }
                    catch (Exception ex)
                    {
                        httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                    }

                    JsonValue responseValue = JsonValue.Parse(httpResponseBody);

                    foreach (JsonValue receiptItemJson in responseValue.GetObject().GetNamedArray("receipt_items"))
                    {
                        JsonObject receiptItemObject = receiptItemJson.GetObject();
                        string itemName = receiptItemObject.GetNamedString("name");
                        double itemPrice = receiptItemObject.GetNamedNumber("price");
                        int itemQty = (int)receiptItemObject.GetNamedNumber("quantity");

                        var receiptItem = new ReceiptItem { Name = itemName, Price = itemPrice, Quantity = itemQty, Receipt = receipt };
                        db.ReceiptItems.Add(receiptItem);
                        db.SaveChanges();
                    }

                    receipt.IsProcessed = true;

                    db.Update(receipt);
                    db.SaveChanges();
                }
            }

            ReloadReceipts();
        }

        private void ReloadReceipts()
        {
            if (ReceiptsView != null)
            {
                ReceiptsView.ReloadReceipts();
            }
        }

        private void OnCompleted(IBackgroundTaskRegistration task, BackgroundTaskCompletedEventArgs args)
        {
            ReloadReceipts();
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            foreach(PivotItem item in PrimaryPivot.Items)
            {
                item.Content = null;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is string)
            {
                var source = (string)e.Parameter;

                if(source == "ReceiptDetails")
                {
                    PrimaryPivot.SelectedIndex = 1;
                } else if(source == "Settings")
                {
                    ReloadReceipts();
                }
            }
        }
    }
}
