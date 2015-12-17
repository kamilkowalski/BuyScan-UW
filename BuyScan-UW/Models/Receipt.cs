using BuyScan_UW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyScan_UW
{
    class Receipt
    {
        public DateTime CreatedAt { get; set; }
        public IList<ReceiptItem> Items { get; set; }

        public double Total()
        {
            double total = 0.0;

            foreach(ReceiptItem item in Items)
            {
                total += item.Price * item.Quantity;
            }

            return total;
        }
    }
}
