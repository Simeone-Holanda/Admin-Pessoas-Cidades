using AdminPersonAndCity.Data.Maps;
using AdminPersonAndCity.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPersonAndCity.Data
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options): base(options) 
        { }

        public DbSet<CityModel> Cities { get; set; }

        public DbSet<PersonModel> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
