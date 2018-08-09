using System;
using System.Collections.Generic;

namespace BrokerService.Database.Models
{
    public partial class PriceCandle
    {
        public long Id { get; set; }
        public string Granularity { get; set; }
        public string Instrument { get; set; }
        public DateTime PriceTime { get; set; }
        public int Volume { get; set; }
        public decimal BidOpen { get; set; }
        public decimal BidHigh { get; set; }
        public decimal BidLow { get; set; }
        public decimal BidClose { get; set; }
        public decimal AskOpen { get; set; }
        public decimal AskHigh { get; set; }
        public decimal AskLow { get; set; }
        public decimal AskClose { get; set; }
    }
}
