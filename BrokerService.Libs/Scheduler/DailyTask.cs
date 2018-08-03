using Microsoft.Extensions.Configuration;
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

        public IConfiguration Configuration { get; }

        public DailyTask(IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        protected override Task DailyCandleTask()
        {
            Console.WriteLine("Running daily candle task");
            return Task.CompletedTask;
        }
    }
}
