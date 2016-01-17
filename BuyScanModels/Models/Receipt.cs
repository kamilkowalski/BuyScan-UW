using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyScanModels.Models
{
    public class Receipt
    {
        public int ReceiptId { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; }
        public string ImagePath { get; set; }
        public bool IsProcessed { get; set; }
        [NotMapped]
        public double Total { get { return GetTotal(); } }
        [NotMapped]
        public bool IsNotProcessed { get { return !IsProcessed; } }
        [NotMapped]
        public string ItemsPreview { get { return GetItemsPreview();  } }

        public Receipt()
        {
            ReceiptItems = new List<ReceiptItem>();
        }

        public double GetTotal()
        {
            double total = 0.0;

            foreach(ReceiptItem item in ReceiptItems)
            {
                total += item.Price * item.Quantity;
            }

            return total;
        }

        private string GetItemsPreview()
        {
            string preview = "";
            bool first = true;
            int charLimit = 20, itemsIncluded = 0, totalCount = ReceiptItems.Count;

            foreach(ReceiptItem item in ReceiptItems)
            {
                if (preview.Length + item.Name.Length >= charLimit)
                {
                    continue;
                }

                if (!first)
                {
                    preview += ", ";
                }
                first = false;
                preview += item.Name;
                itemsIncluded++;
            }

            int itemsDiff = totalCount - itemsIncluded;

            if(itemsDiff > 0)
            {
                preview += ", +" + itemsDiff;
            }

            return preview;
        }
    }
}
