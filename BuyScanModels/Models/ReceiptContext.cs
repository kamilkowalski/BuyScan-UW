using Microsoft.Data.Entity;

namespace BuyScanModels.Models
{
    public class ReceiptContext : DbContext
    {
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptItem> ReceiptItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=Receipts.db");
        }
    }
}
