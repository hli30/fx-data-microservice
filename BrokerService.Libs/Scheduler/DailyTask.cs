using BrokerService.Database.Models;
using BrokerService.Database.Operations.Read;
using BrokerService.Libs.DataFetcher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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
                //Sets a default starting date
                DateTime from = DateTime.UtcNow;

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
                    Console.WriteLine("Error in getting most recent db entry");
                    throw;
                }
                
                List<PriceCandle> priceCandle = _priceDataFetcher.GetDailyData("Oanda", from);

                var context = scope.ServiceProvider.GetRequiredService<Broker_Data_ServiceContext>();

                foreach (var candle in priceCandle)
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
