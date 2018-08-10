using BrokerService.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrokerService.Database.Operations.Read
{
    public class ReadOps : IReadOps
    {
        private readonly Broker_Data_ServiceContext _context;

        public ReadOps(Broker_Data_ServiceContext context)
        {
            _context = context;
        }

        public PriceCandle FindLatestEntryForPair(string fxPair)
        {
            var entry = _context.PriceCandle
                .Where(row => row.Instrument == fxPair)
                .OrderByDescending(row => row.PriceTime)
                .First();

            return entry;
        }
    }
}
