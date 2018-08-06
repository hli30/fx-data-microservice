using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.Brokers.Oanda.JsonResponse
{
    public class Bid
    {
        [JsonProperty("o")]
        public string Open { get; set; }
        [JsonProperty("h")]
        public string High { get; set; }
        [JsonProperty("l")]
        public string Low { get; set; }
        [JsonProperty("c")]
        public string Close { get; set; }
    }

    public class Ask
    {
        [JsonProperty("o")]
        public string Open { get; set; }
        [JsonProperty("h")]
        public string High { get; set; }
        [JsonProperty("l")]
        public string Low { get; set; }
        [JsonProperty("c")]
        public string Close { get; set; }
    }

    public class Candle
    {
        public bool Complete { get; set; }
        public int Volume { get; set; }
        public string Time { get; set; }
        public Bid Bid { get; set; }
        public Ask Ask { get; set; }
    }

    public class CandleJson
    {
        public string Instrument { get; set; }
        public string Granularity { get; set; }
        public List<Candle> Candles { get; set; }
    }
}
