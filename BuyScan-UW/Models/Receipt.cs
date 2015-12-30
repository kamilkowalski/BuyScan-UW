using BuyScan_UW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace BuyScan_UW
{
    class Receipt
    {
        public int ReceiptId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DbSet<ReceiptItem> ReceiptItems { get; set; }
        public string ImagePath { get; set; }

        public double Total()
        {
            double total = 0.0;

            foreach(ReceiptItem item in ReceiptItems)
            {
                total += item.Price * item.Quantity;
            }

            return total;
        }
    }
}
