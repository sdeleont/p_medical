using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Servicios.BackgroundServices
{
    public class GracePeriodManagerService : BackgroundService
    {
        private readonly ILogger<GracePeriodManagerService> _logger;
        //private readonly OrderingBackgroundSettings _settings;

        //private readonly IEventBus _eventBus;

        public GracePeriodManagerService(ILogger<GracePeriodManagerService> logger)
        {
            // Constructor's parameters validations...
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_logger.LogDebug($"GracePeriodManagerService is starting.");

            /*
             stoppingToken.Register(() =>
                _logger.LogDebug($" GracePeriod background task is stopping."));
            */
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogDebug($"GracePeriod task doing background work.");

                // This eShopOnContainers method is querying a database table
                // and publishing events into the Event Bus (RabbitMQ / ServiceBus)
                // CheckConfirmedGracePeriodOrders();
                _logger.LogInformation($"GracePeriod task doing background work.");
                await Task.Delay(TimeSpan.FromMinutes(0.5));
            }

            //_logger.LogDebug($"GracePeriod background task is stopping.");
        }

    }
}
