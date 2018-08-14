using BrokerService.Database.Models;
using BrokerService.Database.Operations.Read;
using BrokerService.Libs.DataFetcher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BrokerService.Libs.Util;

namespace BrokerService.Libs.Scheduler
{
    public class DailyTask : ScheduleProcessor
    {
        //runs on the minute for testing
        protected override string Schedule => "*/1 * * * *";
        //runs Sun to Fri at 21:15 
        //protected override string Schedule => "15 21 * * 0-5";

        private IPriceDataFetcher _priceDataFetcher;
        private readonly IServiceProvider _provider;

        public DailyTask(IPriceDataFetcher priceDataFetcher, IServiceProvider provider) : base()
        {
            _priceDataFetcher = priceDataFetcher;
            _provider = provider;
        }

        //Helper function to aggregate data received from multiple calls when range exceeding maximum candles per call
        private List<PriceCandle> FetchData (string broker, string granularity, DateTime from)
        {
            List<PriceCandle> priceCandles = _priceDataFetcher.GetPriceCandles(broker, granularity, from);

            if (priceCandles.Count == 5000)
            {
                var lastCandleDate = priceCandles[priceCandles.Count - 1].PriceTime.AddDays(1);
                var data = FetchData(broker, granularity, lastCandleDate);
                priceCandles.AddRange(data);
            }

            return priceCandles;
        }

        protected override Task PriceCandleTask()
        {
            Console.WriteLine("Running daily candle task");

            using (IServiceScope scope = _provider.CreateScope())
            {
                //Sets a default starting date
                DateTime from = new DateTime(2000, 1, 1);

                try
                {
                    // Finds the timestamp for the latest db entry and offset the date by
                    // +1 day, in order to fetch data starting from the next available day
                    var readOps = scope.ServiceProvider.GetRequiredService<IReadOps>();

                    PriceCandle latestCandle = readOps.FindLatestEntryForPair("EUR_USD");

                    from = latestCandle.PriceTime.AddDays(1).ToUniversalTime();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in getting most recent db entry, using default start date");
                }

                List<PriceCandle> priceCandles = FetchData("Oanda", "D", from);

                List<PriceCandle> validatedCandles = Validator.ContinuousDataCheck(priceCandles);

                var context = scope.ServiceProvider.GetRequiredService<Broker_Data_ServiceContext>();

                foreach (var candle in validatedCandles)
                {
                    try
                    {
                        context.PriceCandle.Add(candle);
                        context.SaveChanges();
                        Console.WriteLine("Saved Success");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Saving data unsuccessful");
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
