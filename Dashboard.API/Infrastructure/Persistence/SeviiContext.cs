using Dashboard.API.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Infrastructure.Persistence
{
    public class SeviiContext : DbContext
    {
        public SeviiContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<ClientService> ClientServices { get; set; }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
