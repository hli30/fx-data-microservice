using BrokerService.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.Brokers
{
    abstract class BrokerConnection
    {
        /// <summary>
        /// Calls broker api for the previous candle bar.
        /// </summary>
        /// <param name="granularity">
        /// Timeframe of candles. 
        /// W = Weekly;
        /// D = Daily;
        /// H = Hourly.
        /// </param>
        public abstract List<PriceCandle> FetchCandles(string granularity);
    }
}
