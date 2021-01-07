using Microsoft.EntityFrameworkCore;

namespace SingleStoreORM.Models
{
	public class AcmeDataContext : DbContext
    {
        public AcmeDataContext(DbContextOptions<AcmeDataContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
