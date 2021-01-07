using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SingleStoreORM.Models;

namespace SingleStoreORM
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			string connstr = Configuration.GetConnectionString("DefaultConnection");
			services.AddHostedService<Worker>();
			services.AddDbContext<AcmeDataContext>(options =>
				options.UseMySQL(connstr));
		}

	}
}
