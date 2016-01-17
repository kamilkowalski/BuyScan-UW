using System.ComponentModel.DataAnnotations.Schema;

namespace BuyScanModels.Models
{
    public class ReceiptItem
    {
        public int ReceiptItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public virtual Receipt Receipt { get; set; }
        [NotMapped]
        public double TotalPrice { get { return Quantity * Price; } }
    }
}
