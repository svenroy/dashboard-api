using Dashboard.API.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Persistence
{
    public class SeviiContext : DbContext
    {
        public SeviiContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        public DbSet<ClientService> ClientServices { get; set; }

        public DbSet<Client> Clients { get; set; }
    }
}
