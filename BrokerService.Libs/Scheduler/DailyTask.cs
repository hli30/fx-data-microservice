using BrokerService.Libs.DataFetcher;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrokerService.Libs.Scheduler
{
    public class DailyTask : ScheduleProcessor
    {
        //runs on the minute for testing
        protected override string Schedule => "*/1 * * * *";
        //runs Sun to Fri at 21:15 
        //protected override string Schedule => "15 21 * * 0-5";

        private IPriceDataFetcher _priceDataFetcher;

        public DailyTask(IPriceDataFetcher priceDataFetcher) : base()
        {
            Console.WriteLine("Constructing dailytask");
            _priceDataFetcher = priceDataFetcher;
        }

        protected override Task DailyCandleTask()
        {
            Console.WriteLine("Running daily candle task");

            var myString = _priceDataFetcher.GetDailyData("Oanda");

            Console.WriteLine(myString);
            return Task.CompletedTask;
        }
    }
}
