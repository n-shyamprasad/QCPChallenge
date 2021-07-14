using System;
using System.Collections.Generic;
using System.IO;
using QCP.Model;

namespace QCP.Component
{
    public class Utils
    {
        public TradeData LoadTradeData(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                TradeData tradeData = TradeData.FromJson(json);
                return tradeData;
            }
        }
        
        public static void DisplayData(List<OrderBook> orderBook)
        {
            foreach (OrderBook ob in orderBook)
            {
                Console.WriteLine("SecurityCode: {0}", ob.SecurityCode);
                Console.WriteLine("|Buy\t\t\t\t\t\t|Sell\t\t\t\t\t\t|");
                Console.WriteLine("|-----------------------------------------------------------------------------------------------|");
                Console.WriteLine("|Volume\t\t\t|Price\t\t\t|Price\t\t\t|Volume\t\t\t|");
                Console.WriteLine("|-----------------------------------------------------------------------------------------------|");

                foreach (Order o in ob.Orders)
                {
                    Console.WriteLine("|{0}\t\t\t|{1}\t\t\t|{2}\t\t\t|{3}\t\t\t|", o.Buy.Volume, o.Buy.Price, o.Sell.Price, o.Sell.Volume);
                }
                Console.WriteLine("|-----------------------------------------------------------------------------------------------|");
                Console.WriteLine("\n");
            }
        }
    }
}
