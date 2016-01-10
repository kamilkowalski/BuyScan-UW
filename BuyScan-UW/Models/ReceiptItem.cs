using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace BuyScan_UW.Models
{
    class ReceiptItem
    {
        public int ReceiptItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
