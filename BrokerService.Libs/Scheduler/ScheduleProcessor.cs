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
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int counter = 0;
            Console.WriteLine("service started, do 1st time setup");

            do
            {
                var now = DateTime.Now;

                if (now > _nextRun)
                {
                    Console.WriteLine("Starting Task");
                    await DailyCandleTask();
                    _nextRun = _schedule.GetNextOccurrence(now);
                    Console.WriteLine("Task complete, reset");
                    counter = 0;
                }
                Console.WriteLine($"waiting {counter} sec");
                await Task.Delay(10000, stoppingToken);
                counter += 10;
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        protected abstract Task DailyCandleTask();
    }
}
