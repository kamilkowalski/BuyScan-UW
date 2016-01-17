using Windows.ApplicationModel.Background;


namespace BuyScanBackgroundTasks
{
    public sealed class FetchReceiptItemsTask : IBackgroundTask
    {

        BackgroundTaskDeferral _deferral = null;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            ReceiptItemsFetcher.Fetch();

            _deferral.Complete();
        }
    }
}
