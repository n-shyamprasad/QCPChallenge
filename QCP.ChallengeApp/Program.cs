using System;
using System.Collections.Generic;
using QCP.Component;
using QCP.Model;
namespace QCP.ChallengeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load data from json
            TradeData tradeData = new Utils().LoadTradeData("data.json");

            Console.WriteLine("#########  Input  #########");
            Utils.DisplayData(tradeData.OrderBook);

            // Opening Price calculation
            List<OrderBook> orderBooks = new Process().ProcessData(tradeData);
           
            Console.WriteLine("\n\n");
            Console.WriteLine("#########  Output  #########");
            Utils.DisplayData(orderBooks);

            Console.ReadLine();
        }
    }
}
