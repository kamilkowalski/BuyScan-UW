using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyScanModels.Models;
using Windows.Data.Json;
using Windows.Web.Http;

namespace BuyScanBackgroundTasks
{
    public sealed class ReceiptItemsFetcher
    {
        public async static void Fetch() {
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
        }
    }
}
