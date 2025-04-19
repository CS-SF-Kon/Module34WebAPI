using Module34WebAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Module34WebAPI.Data;

public sealed class Module34WebAPIContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Device> Devices { get; set; }

    public Module34WebAPIContext(DbContextOptions<Module34WebAPIContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().ToTable("Rooms");
        modelBuilder.Entity<Device>().ToTable("Devices");
    }
}
