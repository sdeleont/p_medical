using Core.Servicios.Interfaces;
using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Servicios.Impl
{
    public class MyBackgroundService : IHostedService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        

        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            CronExpression expression = CronExpression.Parse("* * * * *");
            DateTimeOffset? next = expression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
            _logger.LogInformation("Starting my service...");
            for (var i = 1; !cancellationToken.IsCancellationRequested; i++)
            {
                _logger.LogInformation($"Loop #{i}");
                ISendEmail email = new SendEmail();
                //await email.SendTest("Number " + i);
                await Task.Delay(TimeSpan.FromMinutes(3));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping my service...");
            return Task.CompletedTask;
        }
    }
}
