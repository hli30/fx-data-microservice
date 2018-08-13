using BrokerService.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Database.Operations.Read
{
    public interface IReadOps
    {
        PriceCandle FindLatestEntryForPair(string fxPair);
    }
}
