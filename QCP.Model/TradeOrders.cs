using System;
using System.Collections.Generic;

using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QCP.Model
{
    public partial class TradeData
    {
        [JsonProperty("OrderBook")]
        public List<OrderBook> OrderBook { get; set; }
    }

    public partial class OrderBook
    {
        [JsonProperty("SecurityCode")]
        public string SecurityCode { get; set; }

        [JsonProperty("Orders")]
        public List<Order> Orders { get; set; }
    }

    public partial class Order
    {
        [JsonProperty("Buy")]
        public Set Buy { get; set; }

        [JsonProperty("Sell")]
        public Set Sell { get; set; }

        public bool SetHigh { get; set; }
        public bool SetOpening { get; set; }
    }

    public partial class Set
    {
        [JsonProperty("Volume")]
        public int Volume { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }
    }

    public partial class TradeData
    {
        public static TradeData FromJson(string json) => JsonConvert.DeserializeObject<TradeData>(json, QCP.Model.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TradeData self) => JsonConvert.SerializeObject(self, QCP.Model.Converter.Settings);
    }



    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
