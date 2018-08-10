using BrokerService.Database.Models;
using BrokerService.Libs.DataFetcher;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using BrokerService.Database.Operations.Read;

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

        protected override Task PriceCandleTask()
        {
            Console.WriteLine("Running daily candle task");

            using (IServiceScope scope = _provider.CreateScope())
            {
                var readOps = scope.ServiceProvider.GetRequiredService<IReadOps>();
                PriceCandle latestCandle = readOps.FindLatestEntryForPair("EUR_USD");
                //get the datetime of latest is from, to is today +15mins, see how many comes back
            }
            List<PriceCandle> priceCandle = _priceDataFetcher.GetDailyData("Oanda");

            using (IServiceScope scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Broker_Data_ServiceContext>();

                foreach (var candle in priceCandle)
                {
                    context.PriceCandle.Add(candle);
                    Console.WriteLine($"Saving data:\n{candle}");
                    context.SaveChanges();
                    Console.WriteLine("Saved Success");
                }
            }

            return Task.CompletedTask;
        }
    }
}
