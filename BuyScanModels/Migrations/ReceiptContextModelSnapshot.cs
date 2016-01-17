using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using BuyScanModels.Models;

namespace BuyScanModels.Migrations
{
    [DbContext(typeof(ReceiptContext))]
    partial class ReceiptContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("BuyScanModels.Models.Receipt", b =>
                {
                    b.Property<int>("ReceiptId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsProcessed");

                    b.Property<string>("Reference");

                    b.HasKey("ReceiptId");
                });

            modelBuilder.Entity("BuyScanModels.Models.ReceiptItem", b =>
                {
                    b.Property<int>("ReceiptItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int?>("ReceiptReceiptId");

                    b.HasKey("ReceiptItemId");
                });

            modelBuilder.Entity("BuyScanModels.Models.ReceiptItem", b =>
                {
                    b.HasOne("BuyScanModels.Models.Receipt")
                        .WithMany()
                        .HasForeignKey("ReceiptReceiptId");
                });
        }
    }
}
