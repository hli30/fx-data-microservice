using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace BrokerService.Libs.Scheduler
{
    public abstract class ScheduleProcessor : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        protected abstract string Schedule { get; }

        public ScheduleProcessor() : base()
        {
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("service started, do 1st time setup");
            await PriceCandleTask();
            await Task.Delay(10000, stoppingToken);
            //do
            //{
            //var now = DateTime.UtcNow;

            //    if (now > _nextRun)
            //    {
            //        Console.WriteLine("Starting Task");
            //        await PriceCandleTask();
            //        _nextRun = _schedule.GetNextOccurrence(now);
            //        Console.WriteLine("Task complete, reset");
            //    }
            //    Console.WriteLine("waiting for next run");
            //    await Task.Delay(10000, stoppingToken);
            //}
            //while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract Task PriceCandleTask();
    }
}
