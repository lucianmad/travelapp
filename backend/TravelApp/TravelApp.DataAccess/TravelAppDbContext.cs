using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TravelApp.DataAccess.Configurations;
using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess;

public class TravelAppDbContext : DbContext
{
    public TravelAppDbContext(DbContextOptions<TravelAppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; } 
    public DbSet<Country> Countries { get; set; } 
    public DbSet<City> Cities { get; set; }
    public DbSet<Attraction> Attractions { get; set; } 
    public DbSet<Review> Reviews { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new CountryConfiguration().Configure(modelBuilder.Entity<Country>());
        new CityConfiguration().Configure(modelBuilder.Entity<City>());
        new AttractionConfiguration().Configure(modelBuilder.Entity<Attraction>());
        new ReviewConfiguration().Configure(modelBuilder.Entity<Review>());

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}