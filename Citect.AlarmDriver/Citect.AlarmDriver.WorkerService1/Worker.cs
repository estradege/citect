using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Citect.AlarmDriver.WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AlarmDbService _db;

        public Worker(ILogger<Worker> logger, AlarmDbService db)
        {
            _logger = logger;
            _db = db;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var e = new Event();
                e.RecordTime = DateTime.Now;
                var events = await _db.GetEventJournalAsync(DateTime.Now.AddHours(-1), DateTime.Now);
                var events2 = events.OrderByDescending(x => x.RecordTime).ToList();

                _logger.LogInformation($"First={events2.First().Message} {events2.First().RecordTime} {events2.First().RecordTime.Kind} ");
                _logger.LogInformation($"Last={events2.Last().Message} {events2.Last().RecordTime} {events2.Last().RecordTime.Kind} ");

                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
