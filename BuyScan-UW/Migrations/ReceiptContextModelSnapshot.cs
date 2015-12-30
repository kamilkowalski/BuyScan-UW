using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using BuyScan_UW.Models;

namespace BuyScan_UW.Migrations
{
    [DbContext(typeof(ReceiptContext))]
    partial class ReceiptContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("BuyScan_UW.Models.ReceiptItem", b =>
                {
                    b.Property<int>("ReceiptItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int?>("ReceiptReceiptId");

                    b.HasKey("ReceiptItemId");
                });

            modelBuilder.Entity("BuyScan_UW.Receipt", b =>
                {
                    b.Property<int>("ReceiptId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("ImagePath");

                    b.HasKey("ReceiptId");
                });

            modelBuilder.Entity("BuyScan_UW.Models.ReceiptItem", b =>
                {
                    b.HasOne("BuyScan_UW.Receipt")
                        .WithMany()
                        .HasForeignKey("ReceiptReceiptId");
                });
        }
    }
}
