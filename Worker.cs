using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SingleStoreORM.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingleStoreORM
{
	public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostLifetime;
		private readonly IServiceScopeFactory _scopeFactory;

        public Worker(IHostApplicationLifetime hostLifetime, IServiceScopeFactory scopeFactory)
        {
            _hostLifetime = hostLifetime ?? throw new ArgumentNullException(nameof(hostLifetime));
			_scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                AcmeDataContext db = scope.ServiceProvider.GetRequiredService<AcmeDataContext>();

                Runner runner = new Runner(db);
                await runner.RunQueries();
            }

            // Run only once:
            _hostLifetime.StopApplication();
        }

    }
}
