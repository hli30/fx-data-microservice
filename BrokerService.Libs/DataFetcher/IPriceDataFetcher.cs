using BrokerService.Database.Models;
using System;
using System.Collections.Generic;

namespace BrokerService.Libs.DataFetcher
{
    public interface IPriceDataFetcher
    {
        List<PriceCandle> GetPriceCandles(string broker, string granularity, DateTime from, string mode = "Practice");
    }
}
