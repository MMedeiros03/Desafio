using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.DataBase;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Price> Prices { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

}
