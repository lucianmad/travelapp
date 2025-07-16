using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelApp.DataAccess.Models;

namespace TravelApp.DataAccess.Configurations;

public class AttractionConfiguration : IEntityTypeConfiguration<Attraction>
{
    public void Configure(EntityTypeBuilder<Attraction> builder)
    {
        builder.ToTable("Attractions");
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Name).IsRequired();
        
        builder.HasOne(a => a.City)
            .WithMany(c => c.Attractions)
            .HasForeignKey(a => a.CityId);
    }
}