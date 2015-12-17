using BuyScan_UW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyScan_UW.MockData
{
    class ReceiptsMock
    {
        static Random rnd = new Random();
        static string[] itemNames = { "Mleko 2%", "Mydło w płynie", "Proszek do prania", "Ziemniaki 5Kg", "Sok jabłkowy Hortex", "Chleb wiejski", "Coca-Cola 1L",
            "Serek wiejski", "Jogurt Jogobella", "Płyn do podłóg Ajax", "Zestaw gąbeczek", "Płyn Pronto", "Szynka z beczki 300g", "Oliwki zielone 200g", "Guma Orbit" };

        public static IList<Receipt> Receipts(int length = 5)
        {
            List<Receipt> receipts = new List<Receipt>(length);

            for(int i=0; i< length; i++)
            {
                receipts.Add(Receipt());
            }

            return receipts;
        }

        public static Receipt Receipt()
        {
            Receipt receipt = new Receipt();
            receipt.CreatedAt = DateTime.Now;
            receipt.Items = ReceiptItems(rnd.Next(3, 10));

            return receipt;
        }

        public static IList<ReceiptItem> ReceiptItems(int length = 5)
        {
            List<ReceiptItem> list = new List<ReceiptItem>(length);

            for(int i=0; i< length; i++)
            {
                list.Add(ReceiptItem());
            }

            return list;
        }

        public static ReceiptItem ReceiptItem()
        {
            ReceiptItem item = new ReceiptItem();
            item.Name = itemNames[rnd.Next(itemNames.Length)];
            item.Quantity = rnd.Next(1, 5);
            item.Price = rnd.NextDouble() * Math.Pow(10, rnd.Next(1, 3));

            return item;
        }
    }
}
