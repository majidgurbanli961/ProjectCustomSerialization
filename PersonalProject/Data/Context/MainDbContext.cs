using Microsoft.EntityFrameworkCore;
using PersonalProject.Data.Entities;

namespace PersonalProject.Data.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
