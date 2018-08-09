using BrokerService.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.DataFetcher
{
    public interface IPriceDataFetcher
    {
        List<PriceCandle> GetDailyData(string broker, string mode = "Practice");
    }
}
