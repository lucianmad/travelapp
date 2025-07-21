using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");
        builder.HasKey(c => c.Id);
        
        builder.HasIndex(c => c.Name).IsUnique();
        
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Flag).HasMaxLength(70);
    }
}