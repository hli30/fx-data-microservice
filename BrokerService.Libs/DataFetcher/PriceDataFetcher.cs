using BrokerService.Database.Models;
using BrokerService.Libs.Brokers;
using BrokerService.Libs.Brokers.Oanda;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.DataFetcher
{
    public class PriceDataFetcher : IPriceDataFetcher
    {
        private readonly IConfiguration _configuration;
        private BrokerFactory _factory;

        public PriceDataFetcher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<PriceCandle> GetDailyData(string broker, string mode = "Practice")
        {
            switch (broker)
            {
                case "Oanda":
                    _factory = new OandaFactory(mode, _configuration);
                    break;
                default:
                    break;
            }

            BrokerConnection connection = _factory.GetBrokerConnection();

            List<PriceCandle> candle = connection.FetchCandles("D");

            return candle;
        }
    }
}
