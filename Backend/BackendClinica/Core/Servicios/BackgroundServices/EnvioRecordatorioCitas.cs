using Core.Modelos.Entorno;
using Core.Servicios.Impl;
using Core.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Servicios.BackgroundServices
{
    public class EnvioRecordatorioCitas: CronJobService
    {
        private readonly ILogger<EnvioRecordatorioCitas> _logger;
        private Configuracion conf;
        public EnvioRecordatorioCitas(IScheduleConfig<EnvioRecordatorioCitas> config, ILogger<EnvioRecordatorioCitas> logger, Configuracion conf)
            : base(config.CronExpression, config.TimeZoneInfo, conf)
        {
            _logger = logger;
            this.conf = conf;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inicializa el proceso Cron");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Realizando proceso de envio de recordatorios");
            ICita cita = new Cita(this.conf);
            cita.EnvioDeRecordatorios();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob 3 is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
