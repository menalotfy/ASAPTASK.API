

using Microsoft.EntityFrameworkCore;

using ASAPTASK.Core.Enitities.AreaEntity;

using ASAPTASK.Core.Enitities.MainEntity;


namespace ASAPTASK.Infrastructure.Data
{
    public class ASAPContext : DbContext
    {
        public ASAPContext()
        {

        }

        public ASAPContext(DbContextOptions<ASAPContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
     


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}
