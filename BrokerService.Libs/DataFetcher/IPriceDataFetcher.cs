using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.DataFetcher
{
    public interface IPriceDataFetcher
    {
        string GetDailyData(string broker, string mode = "Practice");
    }
}
