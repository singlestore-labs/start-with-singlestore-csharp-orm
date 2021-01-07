using Microsoft.Extensions.Hosting;

namespace SingleStoreORM
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) =>
				{
					Startup startup = new Startup(hostContext.Configuration);
					startup.ConfigureServices(services);
				});
	}
}
