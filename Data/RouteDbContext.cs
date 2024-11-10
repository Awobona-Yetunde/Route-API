using Microsoft.EntityFrameworkCore;
using RouteAPi.Models.Domain;

namespace RouteAPi.Data
{
    public class RouteDbContext : DbContext
    {
        public RouteDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<Models.Domain.Route> Routes { get; set; }
    }

   
}
