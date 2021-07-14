using System;
using System.Collections.Generic;
using System.Text;
using QCP.Model;

namespace QCP.Component
{
    public class Process
    {
        public List<OrderBook> ProcessData(TradeData tradeData)
        {
            List<Order> TempOrder = new List<Order>();           
            List<Order> OtherTradeData = new List<Order>();
            OrderBook orderBook = new OrderBook();
            List<OrderBook> OutputList = new List<OrderBook>();
            int iBuy = 0, iSell = 0;

            foreach (OrderBook ob in tradeData.OrderBook)
            {
                orderBook = ob;

                foreach (Order o in ob.Orders)
                {
                    if (o.Sell.Price > o.Buy.Price)
                    {
                        o.SetHigh = true;
                        OtherTradeData = TempOrder;
                        Order order = null;
                        if (OtherTradeData.Count > 0 && !OtherTradeData[OtherTradeData.Count - 1].SetOpening && !OtherTradeData[OtherTradeData.Count - 1].SetHigh)
                        {
                            foreach (Order to in OtherTradeData)
                            {
                                if (!to.SetOpening)
                                {
                                    iBuy += to.Buy.Volume;
                                    iSell += to.Sell.Volume;
                                    order = OtherTradeData[OtherTradeData.Count - 1];
                                }
                                else
                                    break;
                            }


                            for (int i = (OtherTradeData.Count - 1); i >= 0; i--)
                            {
                                if (!OtherTradeData[i].SetOpening)
                                {
                                    OtherTradeData.RemoveAt(i);
                                }
                                else
                                    break;
                            }

                            if (order != null)
                            {
                                order.Buy.Price = order.Sell.Price;
                                order.Buy.Volume = iSell;
                                order.Sell.Volume = iSell;
                                order.SetOpening = true;
                                OtherTradeData.Add(order);
                                o.Buy.Volume = (o.Buy.Volume + iBuy) - iSell;
                            }
                        }
                    }
                    TempOrder.Add(o);
                }
                orderBook.Orders.Clear();
                orderBook.Orders.AddRange(OtherTradeData);
                OutputList.Add(orderBook);
            }
            return OutputList;
        }
    }
}
