using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.DataBase;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Price> Prices { get; set; }
    public DbSet<Parking> Parkings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
#if TEST
        SeedTest.OnModelCreating(builder);
#else
        base.OnModelCreating(builder);
#endif
    }

}
